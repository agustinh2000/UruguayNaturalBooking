using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using RepositoryException;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class TouristSpotManagement : ITouristSpotManagement
    {
        private readonly IRepository<TouristSpot> touristSpotRepository;
        private readonly IRegionManagement regionManagementLogic;
        private readonly ICategoryManagement categoryManagementLogic; 

        public TouristSpotManagement(IRepository<TouristSpot> repository, IRegionManagement regionLogic, ICategoryManagement categoryLogic)
        {
            touristSpotRepository = repository;
            regionManagementLogic = regionLogic;
            categoryManagementLogic = categoryLogic;
        }

        public TouristSpot Create(TouristSpot touristSpot, Guid regionId, List<Guid> categoriesId)
        {
            try
            {
                touristSpot.VerifyFormat();
                touristSpot.Id = Guid.NewGuid();
                Region regionForTouristSpot = regionManagementLogic.GetById(regionId);
                touristSpot.Region = regionForTouristSpot;
                List<Category> listOfCategoriesToAdd = categoryManagementLogic.GetAssociatedCategories(categoriesId); 
                foreach(Category category in listOfCategoriesToAdd)
                {
                    CategoryTouristSpot categoryTouristSpot = new CategoryTouristSpot()
                    {
                        TouristSpot = touristSpot,
                        TouristSpotId = touristSpot.Id,
                        Category = category,
                        CategoryId = category.Id
                    };
                    touristSpot.ListOfCategories.Add(categoryTouristSpot); 
                }
                touristSpotRepository.Add(touristSpot);
                touristSpotRepository.Save();
                return touristSpot;
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede crear el punto turistico debido a que no es valido.", e);
            }
        }

    }
}
