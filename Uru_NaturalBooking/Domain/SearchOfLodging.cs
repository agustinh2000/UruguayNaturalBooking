using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class SearchOfLodging
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int [] QuantityOfGuest { get; set; }

        public TouristSpot TouristSpotSearched { get; set; }

    }
}
