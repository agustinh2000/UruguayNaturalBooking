﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ForRequest
{
    public class LodgingModelForRequest : ModelBaseForRequest<Lodging, LodgingModelForRequest >
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int QuantityOfStars { get; set; }

        public string Address { get; set; }

        public List<string> Images { get; set; }

        public double PricePerNight { get; set; }

        public bool IsAvailable { get; set; } = true; 

        public Guid TouristSpotId { get; set; }

        public override Lodging ToEntity() => new Lodging()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            QuantityOfStars = QuantityOfStars,
            Address = Address,
            PricePerNight = PricePerNight, 
            IsAvailable= IsAvailable
        }; 
    }
}
