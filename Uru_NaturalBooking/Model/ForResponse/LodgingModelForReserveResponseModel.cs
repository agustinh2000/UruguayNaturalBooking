using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ForResponse
{
    public class LodgingModelForReserveResponseModel : ModelBaseForResponse<Lodging, LodgingModelForReserveResponseModel>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        protected override LodgingModelForReserveResponseModel SetModel(Lodging entity)
        {
            Name = entity.Name;
            Address = entity.Address;
            return this; 
        }

        public override bool Equals(object obj)
        {
            return obj is LodgingModelForReserveResponseModel model &&
                   Name == model.Name &&
                   Address == model.Address;
        }

    }
}
