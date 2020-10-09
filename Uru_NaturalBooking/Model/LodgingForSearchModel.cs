using Domain;
using Model.ForResponse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class LodgingForSearchModel 
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int[] QuantityOfGuest { get; set; }

        public LodgingModelForResponse Lodging { get; set; }

        public double TotalPriceForSearch { get; set; }

        public LodgingForSearchModel(){}

        public IEnumerable<LodgingForSearchModel> ToModel(IEnumerable<Lodging> entities)
        {
            List<Lodging> entitiesInList = entities.ToList();
            List<LodgingForSearchModel> toReturn = new List<LodgingForSearchModel>();
            foreach (Lodging entity in entitiesInList)
            {
                int totalDays = (CheckOut - CheckIn).Days;
                LodgingForSearchModel lodging = new LodgingForSearchModel()
                {
                    CheckIn = CheckIn,
                    CheckOut=CheckOut,
                    Lodging = LodgingModelForResponse.ToModel(entity),
                    TotalPriceForSearch = entity.CalculateTotalPrice(totalDays, QuantityOfGuest)
                };
                toReturn.Add(lodging);
            }
            toReturn.AsEnumerable();
            return toReturn;
        }

        public override bool Equals(object obj)
        {
            return obj is LodgingForSearchModel model &&
                   CheckIn == model.CheckIn &&
                   CheckOut == model.CheckOut &&
                   EqualityComparer<LodgingModelForResponse>.Default.Equals(Lodging, model.Lodging);
        }
    }
}
