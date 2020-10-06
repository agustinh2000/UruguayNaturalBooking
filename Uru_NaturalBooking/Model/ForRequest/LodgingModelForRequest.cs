using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ForRequest
{
    public class LodgingModelForRequest : ModelBaseForRequest<Lodging, LodgingModelForRequest >
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
    }
}
