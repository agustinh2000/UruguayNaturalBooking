using DomainException;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual List<LodgingPicture> Images { get; set; }

        public double PricePerNight { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreationDate { get; set; }

        public virtual TouristSpot TouristSpot { get; set; }

        public virtual List <Reserve> ReservesForLodging { get;  set; }

        public virtual List<Review> Reviews { get; set; }

        public double ReviewsAverageScore { get; set; }

        public Lodging()
        {
            Images = new List<LodgingPicture>();
            Reviews = new List<Review>();
            ReservesForLodging = new List<Reserve>();
            CreationDate = DateTime.Now; 
        }

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
            return Images == null || Images.Count == 0;
        }

        public double CalculateTotalPrice(int totalDaysToStay, int[] quantityOfGuest)
        {
            int quantityOfAdults = quantityOfGuest[0];
            int quantityOfChilds = quantityOfGuest[1];
            int quantityOfBabys = quantityOfGuest[2];
            int quantityOfRetired = quantityOfGuest[3];
            const double discountForChilds = 0.50;
            const double discountForBabys = 0.25;
            const double discountForRetireds = 0.70;
            double quantityOfRetiredToApplyDiscount = Math.Floor(quantityOfRetired / 2.0);
            double quantityOfRetiredWithOutDiscount = quantityOfRetired - quantityOfRetiredToApplyDiscount;
            return (PricePerNight * totalDaysToStay) * (quantityOfAdults + (quantityOfChilds * discountForChilds)
                + (quantityOfBabys * discountForBabys) + (quantityOfRetiredToApplyDiscount * discountForRetireds) +
                quantityOfRetiredWithOutDiscount);
        }

        public void UpdateAttributes(Lodging aLodging)
        {
            Name = aLodging.Name;
            QuantityOfStars = aLodging.QuantityOfStars;
            Description = aLodging.Description;
            Address = aLodging.Address;
            IsAvailable = aLodging.IsAvailable; 
        }

        public override bool Equals(object obj)
        {
            return obj is Lodging lodging &&
                   Id.Equals(lodging.Id) &&
                   Name.Equals(lodging.Name) &&
                   QuantityOfStars == lodging.QuantityOfStars &&
                   Address.Equals(lodging.Address) &&
                   PricePerNight == lodging.PricePerNight &&
                   ReviewsAverageScore == lodging.ReviewsAverageScore;
        }

        public int QuantityOfReserveForThePeriod(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            return ReservesForLodging.Where(x => x.CheckIn >= dateCheckIn && x.CheckOut <= dateCheckOut 
            && x.StateOfReserve != Reserve.ReserveState.Expirada && x.StateOfReserve != Reserve.ReserveState.Rechazada).Count();
        }

    }
}
