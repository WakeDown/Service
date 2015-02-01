using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using ZipClaim.Models;
using ZipClaim.Objects;

namespace ZipClaim.Helpers
{
    public class AdHelper
    {
        public static NetworkCredential GetNetCredential4Ad()
        {
            //UN1T\service.ad-admin 1qazXSW@
            string accUserName = ConfigurationManager.AppSettings["userName4Ad"];
            string accUserPass = ConfigurationManager.AppSettings["userPass4Ad"];

            string domain = accUserName.Substring(0, accUserName.IndexOf("\\"));
            string name = accUserName.Substring(accUserName.IndexOf("\\") + 1);

            NetworkCredential nc = new NetworkCredential(name, accUserPass, domain);

            return nc;
        }

        public static void AddUserToGroup(string userSid, string groupName)
        {
            NetworkCredential nc = GetNetCredential4Ad();

            using (WindowsImpersonationContextFacade impersonationContext = new WindowsImpersonationContextFacade(nc))
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                    try
                    {
                        GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName);

                        group.Members.Add(pc, IdentityType.Sid, userSid);

                        //bool flag = false;

                        //try
                        //{
                        //    group.Members.Add(pc, IdentityType.UserPrincipalName, String.Format("{0}@UN1T.GROUP", userSid));
                        //}
                        //catch (NoMatchingPrincipalException ex)
                        //{
                        //    flag = true;
                        //}
                        //try
                        //{
                        //    group.Members.Add(pc, IdentityType.UserPrincipalName, String.Format("{0}@unitgroup.ru", userSid));
                        //    flag = false;
                        //}
                        //catch (NoMatchingPrincipalException ex)
                        //{
                        //    if (flag) throw ex;
                        //}

                        group.Save();
                    }
                    catch (PrincipalExistsException ex)
                    {

                    }

                    
                }
            }
        }

        //public static bool CheckUserGroup(int userId, string groupName)
        //{
        //    NetworkCredential nc = GetNetCredential4Ad();

        //    bool result = false;

        //    using (WindowsImpersonationContextFacade impersonationContext = new WindowsImpersonationContextFacade(nc))
        //    {
        //        using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
        //        {
        //            GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName);
        //            //bool flagUnitG = false;
        //            //    bool flagUnit = false;
        //            bool flag = false;

        //            try
        //            {

                        
        //                try
        //                {
        //                    UserPrincipal user = UserPrincipal.FindByIdentity(pc,
        //                        String.Format("{0}@UN1T.GROUP", userId));
        //                    result = user.IsMemberOf(group);
        //                    flag = true;
        //                }
        //                catch (NoMatchingPrincipalException ex)
        //                {
        //                    flag = false;
        //                }

        //                if (!flag)
        //                {
        //                    try
        //                    {
        //                        UserPrincipal user = UserPrincipal.FindByIdentity(pc,
        //                            String.Format("{0}@unitgroup.ru", userId));
        //                        result = user.IsMemberOf(group);
        //                        flag = true;
        //                    }
        //                    catch (NoMatchingPrincipalException ex)
        //                    {
        //                        if (!flag) throw new Exception();
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                result = false;
        //            }

        //            //if (flagUnitG || flagUnit)

        //            return result;

        //            //try
        //            //{
        //            //    return user.IsMemberOf(group);
        //            //}
        //            //catch (Exception exception)
        //            //{
        //            //    return false;
        //            //}
        //        }
        //    }
        //}

        public static string[] GetGroupMembers(string groupName)
        {
            NetworkCredential nc = GetNetCredential4Ad();
            List<string> lstMembers = new List<string>();

            using (WindowsImpersonationContextFacade impersonationContext = new WindowsImpersonationContextFacade(nc))
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                        GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName);
                        var memb = group.GetMembers(true);

                        foreach (var m in memb)
                    {
                        lstMembers.Add(m.DisplayName);
                    }
                }
            }

            return lstMembers.ToArray();
        }

        public static void RemoveUserFromGroup(string userSid, string groupName)
        {
            NetworkCredential nc = GetNetCredential4Ad();

            using (WindowsImpersonationContextFacade impersonationContext = new WindowsImpersonationContextFacade(nc))
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain))
                {
                    try
                    {
                        GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName);
                        group.Members.Remove(pc, IdentityType.Sid, userSid);
                        //bool flag = false;

                        //try
                        //{
                        //    group.Members.Add(pc, IdentityType.UserPrincipalName, String.Format("{0}@UN1T.GROUP", userId));
                        //}
                        //catch (NoMatchingPrincipalException ex)
                        //{
                        //    flag = true;
                        //}
                        //if (flag)
                        //{
                        //    try
                        //    {
                        //        group.Members.Add(pc, IdentityType.UserPrincipalName,
                        //            String.Format("{0}@unitgroup.ru", userId));
                        //        flag = false;
                        //    }
                        //    catch (NoMatchingPrincipalException ex)
                        //    {
                        //        if (flag) throw ex;
                        //    }
                        //}



                        group.Save();
                        
                    }
                    catch (PrincipalException ex)
                    {


                    }
                }
            }
        }

        public static List<AdGroup> GetUserAdGroups(User user)
        {
            List<AdGroup> result = new List<AdGroup>();

            // establish domain context
            PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

            // find your user
            UserPrincipal up = UserPrincipal.FindByIdentity(yourDomain, user.Login);

            // if found - grab its groups
            if (up != null)
            {
                //PrincipalSearchResult<Principal> groups = up.GetAuthorizationGroups();

                //// iterate over all groups
                //foreach (Principal p in groups)
                //{
                //    // make sure to add only group principals
                //        if (p is GroupPrincipal)
                //        {
                //            result.Add(new AdGroup() { SID = p.Sid.Value, Name = p.DisplayName });
                //        }
                //}

                PrincipalSearchResult<Principal> groups = up.GetAuthorizationGroups();

                var iterGroup = groups.GetEnumerator();
                using (iterGroup)
                {
                    while (iterGroup.MoveNext())
                    {
                        try
                        {
                            Principal p = iterGroup.Current;
                            //result.Add((GroupPrincipal)p);
                            result.Add(new AdGroup() { SID = p.Sid.Value, Name = p.DisplayName });
                        }
                        catch (PrincipalOperationException)
                        {
                            continue;
                        }
                    }
                }
            }

            return result;
        }
    }
}