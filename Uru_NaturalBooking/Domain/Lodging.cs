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

        public string Description { get; set; }

        public virtual List <Picture> Images { get; set; }
        
        public double PricePerNight { get; set; }

        public bool IsAvailable { get; set; } = true; 

        public virtual TouristSpot TouristSpot { get; set; }

        public void VerifyFormat()
        {
            if (IsInvalidNameOrAddressOrDesription())
            {
                throw new LodgingException(MessageExceptionDomain.ErrorIsEmpty); 
            }

            if (IsInvalidQuantityOfStars())
            {
                throw new LodgingException(MessageExceptionDomain.ErrorQuantity); 
            }

            if (IsInvalidadPricePerNight())
            {
                throw new LodgingException(MessageExceptionDomain.ErrorPrice); 
            }

            if (IsInvalidListOfPictures())
            {
                throw new LodgingException(MessageExceptionDomain.ErrorListPictures); 
            }

            TouristSpot.VerifyFormat(); 
        }

        private bool IsInvalidNameOrAddressOrDesription()
        {
            return String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Address) || String.IsNullOrEmpty(Description); 
        }

        private bool IsInvalidQuantityOfStars()
        {
            return QuantityOfStars <= 0 || QuantityOfStars > 5; 
        }

        private bool IsInvalidadPricePerNight()
        {
            return PricePerNight < 0 || PricePerNight > 100000; 
        }

        private bool IsInvalidListOfPictures()
        {
            return Images==null || Images.Count == 0 ; 
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

        public void UpdateAttributes(Lodging aLodging)
        {
            Name = aLodging.Name;
            QuantityOfStars = aLodging.QuantityOfStars;
            Address = aLodging.Address;
            Images = aLodging.Images;
            PricePerNight = aLodging.PricePerNight;
            TouristSpot = aLodging.TouristSpot;
            IsAvailable = aLodging.IsAvailable; 
        }

        public override bool Equals(object obj)
        {
            return obj is Lodging lodging &&
                   Id.Equals(lodging.Id) &&
                   Name.Equals(lodging.Name) &&
                   QuantityOfStars == lodging.QuantityOfStars &&
                   Address.Equals(lodging.Address) &&
                   PricePerNight == lodging.PricePerNight;
        }
    }
}
