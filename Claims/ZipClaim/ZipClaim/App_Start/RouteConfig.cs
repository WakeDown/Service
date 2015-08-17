using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace ZipClaim
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            
            routes.MapPageRoute("", "", "~/WebForms/Claims/List.aspx");
            routes.MapPageRoute("ClaimList", "Claims", "~/WebForms/Claims/List.aspx");
            routes.MapPageRoute("ClaimEditor", "Claims/Editor", "~/WebForms/Claims/Editor.aspx");
            routes.MapPageRoute("ClaimZipHistory", "Claims/ZipHistory", "~/WebForms/Claims/ZipHistory.aspx");
            routes.MapPageRoute("SupplyPriceRequest", "Claims/Supply", "~/WebForms/Claims/Supply.aspx");

            routes.MapPageRoute("ClientList", "Client", "~/WebForms/Client/List.aspx");
            routes.MapPageRoute("ClientDetail", "Client/Detail", "~/WebForms/Client/Detail.aspx");

            routes.MapPageRoute("ClientCounterList", "ClientCounter", "~/WebForms/Client/ListCounter.aspx");
            routes.MapPageRoute("ClientCounterDetail", "Client/CounterDetail", "~/WebForms/Client/CounterDetail.aspx");

            routes.MapPageRoute("ZipGroups", "Settings/ZipGroups", "~/WebForms/Settings/ZipGroups.aspx");
            routes.MapPageRoute("PriceImport", "Settings/Price", "~/WebForms/Settings/PriceImport.aspx");
            routes.MapPageRoute("ClientAccess", "Settings/ClientAccess", "~/WebForms/Settings/ClientAccess.aspx");
            routes.MapPageRoute("ManagerOperator", "Settings/ManagerOperator", "~/WebForms/Settings/ManagerOperator.aspx");

            routes.MapPageRoute("ClaimReport", "Reports/ClaimReport", "~/WebForms/Reports/ClaimReport.aspx");
            //

            routes.MapPageRoute("Error", "Error", "~/WebForms/Error.aspx");
            routes.MapPageRoute("ErrorGrp", "ErrorGrp", "~/WebForms/ErrorGrp.aspx");

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
