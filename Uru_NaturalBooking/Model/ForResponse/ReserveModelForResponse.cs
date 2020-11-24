using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Reserve;

namespace Model.ForResponse
{
    public class ReserveModelForResponse : ModelBaseForResponse<Reserve, ReserveModelForResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int PhoneNumberOfContact { get; set; }

        public string DescriptionForGuest { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int QuantityOfAdult { get; set; }

        public int QuantityOfChild { get; set; }

        public int QuantityOfBaby { get; set; }

        public int QuantityOfRetired { get; set; }

        public ReserveState StateOfReserve { get; set; }

        public string DescriptionOfState { get; set; }

        public LodgingModelForReserveResponseModel Lodging { get; set; }

        public double TotalPrice { get; set; }

        public ReserveModelForResponse() { }

        protected override ReserveModelForResponse SetModel(Reserve entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            LastName = entity.LastName;
            Email = entity.Email;
            PhoneNumberOfContact = entity.PhoneNumberOfContact;
            DescriptionForGuest = entity.DescriptionForGuest;
            CheckIn = entity.CheckIn;
            CheckOut = entity.CheckOut;
            QuantityOfAdult = entity.QuantityOfAdult;
            QuantityOfChild = entity.QuantityOfChild;
            QuantityOfBaby = entity.QuantityOfBaby;
            QuantityOfRetired = entity.QuantityOfRetired;
            StateOfReserve = entity.StateOfReserve;
            DescriptionOfState = entity.GetEnumDescription();
            Lodging = LodgingModelForReserveResponseModel.ToModel(entity.LodgingOfReserve);
            TotalPrice = entity.TotalPrice;
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is ReserveModelForResponse response &&
                   Name == response.Name &&
                   LastName == response.LastName &&
                   Email == response.Email &&
                   CheckIn == response.CheckIn &&
                   CheckOut == response.CheckOut &&
                   QuantityOfAdult == response.QuantityOfAdult &&
                   QuantityOfChild == response.QuantityOfChild &&
                   QuantityOfBaby == response.QuantityOfBaby &&
                   QuantityOfRetired == response.QuantityOfRetired &&
                   TotalPrice == response.TotalPrice &&
                   EqualityComparer<LodgingModelForReserveResponseModel>.Default.Equals(Lodging, response.Lodging);
        }
    }
}
