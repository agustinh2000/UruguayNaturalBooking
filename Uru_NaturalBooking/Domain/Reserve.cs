using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Reserve
    {
        
        public int Id { get; set;}

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public string DescriptionOfGuest { get; set; }

        public virtual Lodging LodgingOfReserve { get; set; }




    }
}
