using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ServiceClaim.Objects;

namespace ServiceClaim.Models
{
    public class ClassifierCaterory : DbModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int Time { get; set; }
        public decimal Price { get; set; }
        public decimal CostPeople { get; set; }
        public decimal CostCompany { get; set; }

        private void FillSelf(ClassifierCaterory model)
        {
            Id = model.Id;
            Name = model.Name;
            Number = model.Number;
            Time = model.Time;
            Price = model.Price;
            CostPeople = model.CostPeople;
            CostCompany = model.CostCompany;
        }

        public static IEnumerable<ClassifierCaterory> GetList()
        {
            Uri uri = new Uri(String.Format("{0}/Classifier/GetList", OdataServiceUri));
            string jsonString = GetJson(uri);
            var model = JsonConvert.DeserializeObject<IEnumerable<ClassifierCaterory>>(jsonString);
            return model;
        }


    }
}