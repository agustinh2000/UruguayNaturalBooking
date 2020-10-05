using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LodgingTouristSpotModel: ModelBase<TouristSpot, LodgingTouristSpotModel>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public LodgingTouristSpotModel() { }

        public LodgingTouristSpotModel(TouristSpot touristSpot)
        {
            SetModel(touristSpot);
        }

        public override TouristSpot ToEntity() => new TouristSpot()
        {
            Id = Id,
            Name = Name
        }; 

        protected override LodgingTouristSpotModel SetModel(TouristSpot touristSpot)
        {
            Id = touristSpot.Id;
            Name = touristSpot.Name;
            return this;
        }
    }
}
