using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using ZipClaim.Objects;

namespace ZipClaim.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string Mail { get; set; }
        public string AdSid { get; set; }
        public bool Enabled { get; set; }
        public EtalonUser EtUser { get; set; }
        public List<AdGroup> AdGroups { get; set; }
        public string Company { get; set; }//Здесь планируется хранить id из Эталона

        public User()
        {
            Id = -1;
                Enabled = true;
                EtUser = new EtalonUser();
                AdGroups = new List<AdGroup>();
            
        }

        public User(string proxySid)
            : this()
        {
            //IIdentity WinId = HttpContext.Current.User.Identity;
            //WindowsIdentity wi = (WindowsIdentity)WinId;
            string sid = proxySid;//wi.User.Value;
            User user = Db.Db.Users.GetUserBySid(sid);
            AdSid = sid;
            Id = user.Id;
            FullName = user.FullName;
            DisplayName = user.DisplayName;
            Login = user.Login;
            Mail = user.Mail;
        }

        /// <summary>
        /// Заполняем данные текущего пользователя
        /// </summary>
        /// <param name="current"></param>
        public User(bool current): this()
        {
            if (current)
            {
                IIdentity WinId = HttpContext.Current.User.Identity;
                WindowsIdentity wi = (WindowsIdentity)WinId;
                string sid = wi.User.Value;
                //sid = "S-1-5-21-1970802976-3466419101-4042325969-2275"; //Поплеухина
                //sid = "S-1-5-21-1970802976-3466419101-4042325969-3360"; //Кареева
                //sid = "S-1-5-21-1970802976-3466419101-4042325969-4496";//Москаленко
                //sid = "S-1-5-21-1970802976-3466419101-4042325969-2275";
                //sid = "S-1-5-21-1970802976-3466419101-4042325969-3704";//UN1T
                //sid = "S-1-5-21-1970802976-3466419101-4042325969-4246";//СвердловскийХлебомакаронныйКомбинат
                //sid="S-1-5-21-1970802976-3466419101-4042325969-4248";//МРСК Урала-Челябэнерго
                //sid = "S-1-5-21-1970802976-3466419101-4042325969-4085";//Турбина СКБ

                User user = Db.Db.Users.GetUserBySid(sid);

                Id = user.Id;
                AdSid = sid;
                FullName = user.FullName;
                DisplayName = user.DisplayName;
                Login = user.Login;
                Mail = user.Mail;
                Company = user.Company;
            }
        }

        public User(int id, string login, string fullName, string displayName, string mail)
            : this()
        {
            Id = id;
            FullName = fullName;
            DisplayName = displayName;
            Login = login;
            Mail = mail;
        }

        public bool CheckRights(string rightName)
        {
           return Db.Db.Users.CheckUserRights(Login, rightName);
        }
    }

    /// <summary>
    /// Пользователь Эталона
    /// </summary>
    [Serializable]
    public class EtalonUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string AdSid { get; set; }

        public EtalonUser()
        {
        }

        public EtalonUser(int id, string login, string password, string displayName)
            : this()
        {
            Id = id;
            Login = login;
            Password = password;
            DisplayName = displayName;
        }
    }
}