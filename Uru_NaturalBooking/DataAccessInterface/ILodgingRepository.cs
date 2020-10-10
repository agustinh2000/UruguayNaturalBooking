using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessInterface
{
    public interface ILodgingRepository : IRepository<Lodging>
    {
        List<Lodging> GetAvailableLodgingsByTouristSpot(Guid touristSpotId);

        Lodging GetLodgingByNameAndTouristSpot(string lodgingName, Guid touristSpotId);
    }
}
