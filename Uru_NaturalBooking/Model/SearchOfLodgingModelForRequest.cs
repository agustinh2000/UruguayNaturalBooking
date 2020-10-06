using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SearchOfLodgingModelForRequest 
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int QuantityOfAdult { get; set; }

        public int QuantityOfChilds { get; set; }

        public int QuantityOfBabys { get; set; }

        public Guid TouristSpotIdSearch { get; set; }
    }
}
