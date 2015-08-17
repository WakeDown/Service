using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ServiceClaim.Objects;

namespace ServiceClaim.Models
{
    public class Claim:DbModel
    {
        public int Id { get; set; }
        public string Sid { get; set; }
        public Contractor Contractor { get; set; }
        public Contract Contract { get; set; }
        public Device Device { get; set; }
        public string ContractorName { get; set; }
        public string ContractName { get; set; }
        public string DeviceName { get; set; }
        public EmployeeSm Admin { get; set; }
        public EmployeeSm Engeneer { get; set; }
        public ClaimState State { get; set; }

        //public IEnumerable<Claim2ClaimState> StateHistory { get; set; }

        public string Descr { get; set; }

        public Claim()
        {
            //Contractor=new Contractor();
            //Contract = new Contract();
            //Device=new Device();
            //Admin=new EmployeeSm();
            //Engeneer=new EmployeeSm();
        }

        public Claim(int id)
        {
            Uri uri = new Uri(String.Format("{0}/Claim/Get?id={1}", OdataServiceUri, id));
            string jsonString = GetJson(uri);
            var model = JsonConvert.DeserializeObject<Claim>(jsonString);
            FillSelf(model);
        }

        private void FillSelf(Claim model)
        {
            Id = model.Id;
            Sid = model.Sid;
            Contractor = model.Contractor;
            Contract = model.Contract;
            Device = model.Device;
            ContractorName = model.ContractorName;
            ContractName = model.ContractName;
            DeviceName = model.DeviceName;
            Admin = model.Admin;
            Engeneer = model.Engeneer;
            State = model.State;
            //StateHistory = model.StateHistory;
        }

        public static IEnumerable<Claim> GetList()
        {
            Uri uri = new Uri(String.Format("{0}/Claim/GetList", OdataServiceUri));
            string jsonString = GetJson(uri);
            var model = JsonConvert.DeserializeObject<IEnumerable<Claim>>(jsonString);
            return model;
        }

        public bool Save(out ResponseMessage responseMessage)
        {
            Uri uri = new Uri(String.Format("{0}/Claim/Save", OdataServiceUri));
            string json = JsonConvert.SerializeObject(this);
            bool result = PostJson(uri, json, out responseMessage);
            return result;
        }

        public bool SaveAndGoNextState(out ResponseMessage responseMessage)
        {
            Uri uri = new Uri(String.Format("{0}/Claim/SaveAndGoNextState", OdataServiceUri));
            string json = JsonConvert.SerializeObject(this);
            bool result = PostJson(uri, json, out responseMessage);
            return result;
        }

        public bool SaveAndGoEndState(out ResponseMessage responseMessage)
        {
            Uri uri = new Uri(String.Format("{0}/Claim/SaveAndGoEndState", OdataServiceUri));
            string json = JsonConvert.SerializeObject(this);
            bool result = PostJson(uri, json, out responseMessage);
            return result;
        }

        public IEnumerable<Claim2ClaimState> GetStateHistory()
        {
            Uri uri = new Uri(String.Format("{0}/Claim/GetStateHistory?id={1}", OdataServiceUri, Id));
            string jsonString = GetJson(uri);
            var model = JsonConvert.DeserializeObject<IEnumerable<Claim2ClaimState>>(jsonString);
            return model;
        }
    }
}