using DomainException;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class TouristSpot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public virtual Region Region { get; set; }

        public virtual List<CategoryTouristSpot> ListOfCategories { get; set; }

        public TouristSpot() {
            ListOfCategories = new List<CategoryTouristSpot>(); 
        }

        public void VerifyFormat()
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Description))
            {
                throw new TouristSpotException(MessageException.ErrorIsEmpty);
            }

            if (IsLongerThanTheLimit())
            {
                throw new TouristSpotException(MessageException.ErrorIsLongerThanTheLimit);
            }
        }

        private bool IsLongerThanTheLimit()
        {
            return Description.Length > 2000;
        }
    }
}
