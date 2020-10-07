using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Reserve;

namespace Model.ForRequest
{
    public class ReserveModelForRequestUpdate : ModelBaseForRequest<Reserve, ReserveModelForRequestUpdate>
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public ReserveState StateOfReserve { get; set; }

        public override Reserve ToEntity() => new Reserve()
        {
            Id= Id, 
            DescriptionForGuest = Description,
            StateOfReserve = StateOfReserve
        }; 
    }
}
