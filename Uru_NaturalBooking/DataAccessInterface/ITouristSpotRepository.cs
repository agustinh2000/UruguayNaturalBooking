using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessInterface
{
    public interface ITouristSpotRepository : IRepository<TouristSpot>
    {
        List<TouristSpot> GetTouristSpotsByCategoriesAndRegion(List<Guid> listOfCategoriesIdSearched, Guid regionIdSearched);

        TouristSpot GetTouristSpotByName(string touristSpotName);
    }
}
