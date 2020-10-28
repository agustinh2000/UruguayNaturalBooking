using DomainException;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Reflection;

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

        public double TotalPrice { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int QuantityOfAdult { get; set; }

        public int QuantityOfChild { get; set; }

        public int QuantityOfBaby { get; set; }

        public int QuantityOfRetired { get; set; }

        public enum ReserveState
        {
            [Description("Creada")]
            Creada,
            [Description("Pendiente de pago")]
            Pendiente_Pago,
            [Description("Aceptada")]
            Aceptada,
            [Description("Rechazada")]
            Rechazada,
            [Description("Expirada")]
            Expirada
        }

        public ReserveState StateOfReserve { get; set; }

        public void VerifyFormat()
        {
            if (IsInvalidNameOrLastName())
            {
                throw new ReserveException(MessageExceptionDomain.ErrorIsEmpty);
            }

            if (IsInvalidDate())
            {
                throw new ReserveException(MessageExceptionDomain.ErrorDate);
            }

            if (IsInvalidQuantityGuest())
            {
                throw new ReserveException(MessageExceptionDomain.ErrorQuantityGuestNegative);
            }

            if (NotHasGuest())
            {
                throw new ReserveException(MessageExceptionDomain.ErrorQuantityOfGuest);
            }

            if (string.IsNullOrEmpty(Email) || !IsValidEmail())
            {
                throw new ReserveException(MessageExceptionDomain.ErrorEmail);
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
            return QuantityOfAdult < 0 || QuantityOfBaby < 0 || QuantityOfChild < 0;
        }

        private bool NotHasGuest()
        {
            return QuantityOfAdult == 0 && QuantityOfBaby == 0 && QuantityOfChild == 0;
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

        public void UpdateAttributes(Reserve infoReserveToUpdate)
        {
            if (!String.IsNullOrEmpty(infoReserveToUpdate.DescriptionForGuest))
            {
                DescriptionForGuest = infoReserveToUpdate.DescriptionForGuest;
            }

            if (Enum.IsDefined(typeof(ReserveState), infoReserveToUpdate.StateOfReserve))
            {
                StateOfReserve = infoReserveToUpdate.StateOfReserve;
            }
        }

        public string GetEnumDescription()
        {
            FieldInfo fi = StateOfReserve.GetType().GetField(StateOfReserve.ToString());
            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            return attributes.First().Description;
        }

        public override bool Equals(object obj)
        {
            return obj is Reserve reserve &&
                   Name.Equals(reserve.Name) &&
                   LastName.Equals(reserve.LastName) &&
                   Email.Equals(reserve.Email) &&
                   TotalPrice.Equals(reserve.TotalPrice) &&
                   EqualityComparer<Lodging>.Default.Equals(LodgingOfReserve, reserve.LodgingOfReserve)
                   && DateTime.Compare(CheckIn, reserve.CheckIn) == 0
                   && DateTime.Compare(CheckOut, reserve.CheckOut) == 0
                   && StateOfReserve.Equals(reserve.StateOfReserve);
        }

    }
}
