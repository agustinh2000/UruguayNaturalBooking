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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                UserSession userSession = (UserSession)obj;
                return Id.Equals(userSession.Id) && Token.Equals(userSession.Token);
            }
        }
    }
}
