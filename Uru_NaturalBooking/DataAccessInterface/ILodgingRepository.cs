using Domain;
using System;
using System.Collections.Generic;

namespace DataAccessInterface
{
    public interface ILodgingRepository : IRepository<Lodging>
    {
        List<Lodging> GetAvailableLodgingsByTouristSpot(Guid touristSpotId);

        Lodging GetLodgingByNameAndTouristSpot(string lodgingName, Guid touristSpotId);

        List<Lodging> GetLodgingsWithReserves(Guid idOfTouristSpot, DateTime dateCheckInMax, DateTime dateCheckOutMax); 
    }
}
