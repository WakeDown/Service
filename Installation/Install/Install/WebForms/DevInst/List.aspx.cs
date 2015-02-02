using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Install.Objects;

namespace Install.WebForms.DevInst
{
    public partial class List : BaseFilteredPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("id", txtId));
            FilterLinks.Add(new FilterLink("sdnum", txtSdNum));
            FilterLinks.Add(new FilterLink("csdnum", txtContractorSdNum));
            FilterLinks.Add(new FilterLink("sadm", ddlServiceAdmin, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("engr", ddlEngeneer, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("snum", txtSerialNum));
            FilterLinks.Add(new FilterLink("ctrtr", ddlContractor));
            FilterLinks.Add(new FilterLink("state", chklClaimState, "1,3,4,5,6,8,9,10,11,12,13,21"));
            //FilterLinks.Add(new FilterLink("etste", chklEtClaimState));//, "10,11,12,13"
            FilterLinks.Add(new FilterLink("wayst", chklWaybillClaimState));//, "14,15,16,18,19,20"
            FilterLinks.Add(new FilterLink("mngr", ddlManager, User.Id.ToString()));
            FilterLinks.Add(new FilterLink("oper", ddlOperator));
            FilterLinks.Add(new FilterLink("dst", txtDateBegin));
            FilterLinks.Add(new FilterLink("den", txtDateEnd));
            FilterLinks.Add(new FilterLink("inn", txtContractorInn));
            FilterLinks.Add(new FilterLink("nins", rblNotInSystem));
            FilterLinks.Add(new FilterLink("rcn", txtRowsCount, "30"));

            BtnSearchClientId = btnSearch.ClientID;
        }
    }
}