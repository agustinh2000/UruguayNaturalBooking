using DomainException;
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

        public bool IsAvailable { get; set; } = true; 

        public virtual TouristSpot TouristSpot { get; set; }

        public void VerifyFormat()
        {
            if (IsInvalidNameOrAddress())
            {
                throw new LodgingException(MessageException.ErrorIsEmpty); 
            }

            if (IsInvalidQuantityOfStars())
            {
                throw new LodgingException(MessageException.ErrorQuantity); 
            }

            if (IsInvalidadPricePerNight())
            {
                throw new LodgingException(MessageException.ErrorPrice); 
            }

            TouristSpot.VerifyFormat(); 
        }

        private bool IsInvalidNameOrAddress()
        {
            return String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Address); 
        }

        private bool IsInvalidQuantityOfStars()
        {
            return QuantityOfStars <= 0 || QuantityOfStars > 5; 
        }

        private bool IsInvalidadPricePerNight()
        {
            return PricePerNight < 0 || PricePerNight > 100000; 
        }

        public double CalculateTotalPrice(int totalDaysToStay, int[] quantityOfGuest)
        {
            int quantityOfAdults = quantityOfGuest[0];
            int quantityOfChilds = quantityOfGuest[1];
            int quantityOfBabys = quantityOfGuest[2];
            const double discountForChilds = 0.50;
            const double discountForBabys = 0.25;
            return (PricePerNight * totalDaysToStay) * (quantityOfAdults + (quantityOfChilds * discountForChilds) 
                + (quantityOfBabys * discountForBabys)); 
        }

        //public void UpdateAttributes(Lodging aLodging)
        //{
        //    Name = aLodging.Name;
        //    QuantityOfStars = aLodging.QuantityOfStars;
        //    Address = aLodging.Address;
        //    Images = aLodging.Images;
        //    PricePerNight = aLodging.PricePerNight;
        //    TouristSpot = aLodging.TouristSpot;
        //}

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Lodging lodging = (Lodging)obj;
                return Id.Equals(lodging.Id) && Name.Equals(lodging.Name)
                    && PricePerNight== lodging.PricePerNight
                    && QuantityOfStars== lodging.QuantityOfStars;
            }
        }

    }
}
