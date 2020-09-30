using DomainException;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Domain
{
    public class Reserve
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int PhoneNumberOfContact { get; set; }

        public string DescriptionForGuest { get; set; }

        public virtual Lodging LodgingOfReserve { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int QuantityOfAdult { get; set; }

        public int QuantityOfChild { get; set;  }

        public int QuantityOfBaby { get; set; }

        public enum ReserveState { Creada, Pendiente_Pago, Aceptada, Rechazada, Expirada }

        public ReserveState StateOfReserve {get; set; }

        public void VerifyFormat()
        {
            if (IsInvalidNameOrLastName())
            {
                throw new ReserveException(MessageException.ErrorIsEmpty); 
            }

            if (IsInvalidDate())
            {
                throw new ReserveException(MessageException.ErrorDate); 
            }

            if (IsInvalidQuantityGuest())
            {
                throw new ReserveException(MessageException.ErrorQuantityOfGuest);
            }

            if (!IsValidEmail())
            {
                throw new ReserveException(MessageException.ErrorEmail); 
            }
            LodgingOfReserve.VerifyFormat(); 
        }

        private bool IsInvalidNameOrLastName()
        {
            return string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName); 
        }

        private bool IsInvalidDate()
        {
            return CheckIn > CheckOut; 
        }

        private bool IsInvalidQuantityGuest()
        {
            return QuantityOfAdult <= 0 && QuantityOfBaby <= 0 && QuantityOfChild <= 0; 
        }

        private bool IsValidEmail()
        {
            try
            {
                MailAddress address = new System.Net.Mail.MailAddress(Email);
                return address.Address.Equals(Email);
            }
            catch (FormatException)
            {
                return false;
            }
        }

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
                Reserve reserve = (Reserve)obj;
                return Name.Equals(reserve.Name) && LastName.Equals(reserve.LastName)
                     && Email.Equals(reserve.Email)
                     && LodgingOfReserve.Equals(reserve.LodgingOfReserve)
                     && DateTime.Compare(CheckIn, reserve.CheckIn) == 0
                     && DateTime.Compare(CheckOut, reserve.CheckOut) == 0; 
            }
        }



    }
}
