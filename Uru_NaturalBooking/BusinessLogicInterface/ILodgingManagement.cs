using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface ILodgingManagement
    {
        Lodging Create(Lodging lodging, Guid touristSpotId, List<string> pathOfPictures);

        Lodging GetLodgingById(Guid lodgingId);

        List<Lodging> GetAvailableLodgingsByTouristSpot(Guid touristSpotId);

        List<Lodging> GetAllLoadings();

        void RemoveLodging(Guid lodgingId);

        Lodging UpdateLodging(Guid idOfLodgingToUpdate, Lodging aLodging);

        List<Lodging> GetLodgingsWithReservesBetweenDates(Guid idOfTouristSpot, DateTime dateCheckInMax, DateTime dateCheckOutMax);
    }
}
