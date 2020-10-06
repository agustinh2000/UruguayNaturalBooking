using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LodgingModelForResponse : ModelBase<Lodging, LodgingModelForResponse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int QuantityOfStars { get; set; }

        public string Address { get; set; }

        public byte[] Images { get; set; }

        public double PricePerNight { get; set; }

        public TouristSpotForLodgingModel LodgingTouristSpotModel { get; set; }

        public LodgingModelForResponse() { }

        public LodgingModelForResponse(Lodging lodging)
        {
            SetModel(lodging);
        }

        public override Lodging ToEntity() => new Lodging()
        {
            Id = Id,
            Name = Name, 
            QuantityOfStars= QuantityOfStars, 
            Address= Address, 
            Images= Images, 
            PricePerNight = PricePerNight
        };

        protected override LodgingModelForResponse SetModel(Lodging lodging)
        {
            Id = lodging.Id;
            Name = lodging.Name;
            QuantityOfStars = lodging.QuantityOfStars;
            Address = lodging.Address;
            Images = lodging.Images;
            PricePerNight = lodging.PricePerNight;
            LodgingTouristSpotModel = TouristSpotForLodgingModel.ToModel(lodging.TouristSpot); 
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is LodgingModelForResponse response &&
                   Id.Equals(response.Id) &&
                   Name == response.Name &&
                   QuantityOfStars == response.QuantityOfStars &&
                   Address == response.Address &&
                   PricePerNight == response.PricePerNight;
        }
    }
}
