using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface ILodgingManagement
    {
        Lodging Create(Lodging lodging, Guid touristSpotId);

        Lodging GetLodgingById(Guid lodgingId);

        List<Lodging> GetLodgingsByTouristSpot(Guid touristSpotId);

        List<Lodging> GetAllLoadings();

        void RemoveLodging(Guid lodgingId);

        Lodging UpdateLodging(Lodging aLodging);
    }
}
