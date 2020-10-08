using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UserSession
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public virtual User User { get; set; }
        public DateTime ConnectedAt { get; set; }

        public UserSession()
        {
            ConnectedAt = DateTime.Now;
        }

    }
}
