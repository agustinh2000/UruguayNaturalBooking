using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface IReserveManagement
    {
        Reserve Create(Reserve reserve, Guid lodgingId); 
    }
}
