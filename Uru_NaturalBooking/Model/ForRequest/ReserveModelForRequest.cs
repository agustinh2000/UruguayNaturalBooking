using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ForRequest
{
    public class ReserveModelForRequest : ModelBaseForRequest<Reserve, ReserveModelForRequest>
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int QuantityOfAdult { get; set; }

        public int QuantityOfChild { get; set; }

        public int QuantityOfBaby { get; set; }

        public Guid IdOfLodgingToReserve { get; set; }

        public ReserveModelForRequest() { }

        public override Reserve ToEntity() => new Reserve()
        {
            Name = Name,
            LastName = LastName,
            Email = Email,
            CheckIn = CheckIn,
            CheckOut = CheckOut,
            QuantityOfAdult = QuantityOfAdult,
            QuantityOfChild = QuantityOfChild,
            QuantityOfBaby = QuantityOfBaby
        }; 
    }
}
