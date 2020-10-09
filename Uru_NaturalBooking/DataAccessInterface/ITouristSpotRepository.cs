using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessInterface
{
    public interface ITouristSpotRepository : IRepository<TouristSpot>
    {
        List<TouristSpot> GetTouristSpotByRegion(Guid regionId);

        List<TouristSpot> GetTouristSpotsByCategories(List<Guid> listOfCategoriesIdSearched);

        List<TouristSpot> GetTouristSpotsByCategoriesAndRegion(List<Guid> listOfCategoriesIdSearched, Guid regionIdSearched);

    }
}
