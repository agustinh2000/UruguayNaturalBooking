using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface ITouristSpotManagement
    {
        TouristSpot Create(TouristSpot touristSpot, Guid regionId, List<Guid> listOfIdentifiers);

        List<TouristSpot> GetTouristSpotByRegion(Guid regionId);

        TouristSpot GetTouristSpotById(Guid touristSpotId);

        List<TouristSpot> GetTouristSpotsByCategories(List<Guid> listOfCategoriesIdSearched);

        List<TouristSpot> GetTouristSpotsByCategoriesAndRegion(List<Guid> listOfCategoriesIdSearched, Guid regionIdSearched);

        TouristSpot UpdateTouristSpot(TouristSpot touristSpot);

        void RemoveTouristSpot(Guid touristSpotId);

        List<TouristSpot> GetAllTouristSpot();
    }
}
