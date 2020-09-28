using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Lodging
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int QuantityOfStars { get; set; }

        public string Address { get; set; }

        public byte [] Images { get; set; }
        
        public double PricePerNight { get; set; }

        public double TotalPrice { get; set; }

        public virtual TouristSpot TouristSpot { get; set; }


    }
}
