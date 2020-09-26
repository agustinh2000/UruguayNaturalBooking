using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; } = false;
        public string Mail { get; set; }

    }
}
