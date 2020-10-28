using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ForResponse
{
    public class ReportLodgingModelForResponse 
    {
        public string NameOfLodging { get; set; }

        public int QuantityOfReserves { get; set; }

        public ReportLodgingModelForResponse() { }
        public IEnumerable<ReportLodgingModelForResponse> SetModel(IEnumerable<Lodging> entities, DateTime checkInMaxDate, DateTime checkOutMaxDate)
        {
            List<Lodging> entitiesInList = entities.ToList();
            List<ReportLodgingModelForResponse> toReturn = new List<ReportLodgingModelForResponse>();
            foreach (Lodging entity in entitiesInList)
            {
                ReportLodgingModelForResponse infoOfReport = new ReportLodgingModelForResponse()
                {
                    NameOfLodging = entity.Name,
                    QuantityOfReserves = entity.QuantityOfReserveForThePeriod(checkInMaxDate, checkOutMaxDate) 
                }; 
                toReturn.Add(infoOfReport);
            }
            toReturn.AsEnumerable();
            return toReturn;
        }

        public override bool Equals(object obj)
        {
            return obj is ReportLodgingModelForResponse reportModel &&
                   NameOfLodging.Equals(reportModel.NameOfLodging) &&
                   QuantityOfReserves == reportModel.QuantityOfReserves;
        }
    }
}
