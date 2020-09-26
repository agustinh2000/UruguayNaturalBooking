using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface ITouristSpotManagement
    {
        TouristSpot Create(TouristSpot touristSpot, Guid regionId, List<Guid> listOfIdentifiers);
    }
}
