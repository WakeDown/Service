using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace Install
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            routes.MapPageRoute("", "", "~/WebForms/DevInst/List.aspx");
            routes.MapPageRoute("DevInstList", "DevInst", "~/WebForms/DevInst/List.aspx");
            routes.MapPageRoute("DevInstEditor", "DevInst/Editor", "~/WebForms/DevInst/Editor.aspx");

            #region Pictures

            //routes.RouteExistingFiles = false;
            routes.MapPageRoute("Chk-unchecked", "Images/Chk_tri_state/unchecked.gif", "~/Images/Chk_tri_state/unchecked.gif");
            routes.MapPageRoute("Chk-checked", "Images/Chk_tri_state/checked.gif", "~/Images/Chk_tri_state/checked.gif");
            routes.MapPageRoute("Chk-intermediate", "Images/Chk_tri_state/intermediate.gif", "~/Images/Chk_tri_state/intermediate.gif");

            routes.MapPageRoute("Chk-unchecked_highlighted", "Images/Chk_tri_state/unchecked_highlighted.gif", "~/Images/Chk_tri_state/unchecked_highlighted.gif");
            routes.MapPageRoute("Chk-checked_highlighted", "Images/Chk_tri_state/checked_highlighted.gif", "~/Images/Chk_tri_state/checked_highlighted.gif");
            routes.MapPageRoute("Chk-intermediate_highlighted", "Images/Chk_tri_state/intermediate_highlighted.gif", "~/Images/Chk_tri_state/intermediate_highlighted.gif");

            #endregion
        }
    }
}
