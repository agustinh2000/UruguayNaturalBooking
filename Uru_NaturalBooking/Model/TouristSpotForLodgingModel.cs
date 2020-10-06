using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TouristSpotForLodgingModel: ModelBase<TouristSpot, TouristSpotForLodgingModel>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TouristSpotForLodgingModel() { }

        public TouristSpotForLodgingModel(TouristSpot touristSpot)
        {
            SetModel(touristSpot);
        }

        public override TouristSpot ToEntity() => new TouristSpot()
        {
            Id = Id,
            Name = Name
        }; 

        protected override TouristSpotForLodgingModel SetModel(TouristSpot touristSpot)
        {
            Id = touristSpot.Id;
            Name = touristSpot.Name;
            return this;
        }
    }
}
