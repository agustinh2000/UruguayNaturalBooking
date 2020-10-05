using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LodgingModelForRequest : ModelBase<Lodging, LodgingModelForRequest >
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int QuantityOfStars { get; set; }

        public string Address { get; set; }

        public byte[] Images { get; set; }

        public double PricePerNight { get; set; }

        public Guid TouristSpotId { get; set; }

        public override Lodging ToEntity() => new Lodging()
        {
            Id = Id,
            Name = Name,
            QuantityOfStars = QuantityOfStars,
            Address = Address,
            Images = Images,
            PricePerNight = PricePerNight
        }; 

        protected override LodgingModelForRequest SetModel(Lodging lodging)
        {
            Id = lodging.Id;
            Name = lodging.Name;
            QuantityOfStars = lodging.QuantityOfStars;
            Address = lodging.Address;
            Images = lodging.Images;
            PricePerNight = lodging.PricePerNight;
            TouristSpotId = lodging.TouristSpot.Id; 
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is LodgingModelForRequest request &&
                   Id.Equals(request.Id) &&
                   Name == request.Name &&
                   QuantityOfStars == request.QuantityOfStars &&
                   Address == request.Address &&
                   PricePerNight == request.PricePerNight;
        }
    }
}
