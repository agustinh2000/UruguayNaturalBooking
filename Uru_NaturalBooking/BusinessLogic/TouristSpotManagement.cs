using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class TouristSpotManagement : ITouristSpotManagement
    {
        private readonly ITouristSpotRepository touristSpotRepository;
        private readonly IRegionManagement regionManagementLogic;
        private readonly ICategoryManagement categoryManagementLogic;

        public TouristSpotManagement(ITouristSpotRepository repository, IRegionManagement regionLogic, ICategoryManagement categoryLogic)
        {
            touristSpotRepository = repository;
            regionManagementLogic = regionLogic;
            categoryManagementLogic = categoryLogic;
        }

        public TouristSpotManagement(ITouristSpotRepository repository)
        {
            touristSpotRepository = repository;
        }

        public TouristSpot Create(TouristSpot touristSpot, Guid regionId, List<Guid> categoriesId)
        {
            try
            {
                touristSpot.Id = Guid.NewGuid();
                Region regionForTouristSpot = regionManagementLogic.GetById(regionId);
                touristSpot.Region = regionForTouristSpot;
                List<Category> listOfCategoriesToAdd = categoryManagementLogic.GetAssociatedCategories(categoriesId);
                foreach (Category category in listOfCategoriesToAdd)
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
                touristSpot.VerifyFormat();
                touristSpotRepository.Add(touristSpot);
                return touristSpot;
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede crear el punto turistico debido a que no es valido.", e);
            }
        }

        public List<TouristSpot> GetTouristSpotByRegion(Guid regionId)
        {
            try
            {
                return touristSpotRepository.GetTouristSpotByRegion(regionId); 
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede obtener el punto turistico por la region.", e);
            }
        }

        public TouristSpot GetTouristSpotById(Guid touristSpotId)
        {
            try
            {
                TouristSpot touristSpotObteined = touristSpotRepository.Get(touristSpotId);
                return touristSpotObteined;
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede obtener el punto turistico atraves del Id.", e);
            }
        }

        public List<TouristSpot> GetTouristSpotsByCategories(List<Guid> listOfCategoriesIdSearched)
        {
            try
            {
                return touristSpotRepository.GetTouristSpotsByCategories(listOfCategoriesIdSearched); 
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede obtener los puntos turisticos que se estan buscando por dichas categorias.", e);
            }
        }

        public List<TouristSpot> GetTouristSpotsByCategoriesAndRegion(List<Guid> listOfCategoriesIdSearched, Guid regionIdSearched)
        {
            try
            {
                return touristSpotRepository.GetTouristSpotsByCategoriesAndRegion(listOfCategoriesIdSearched, regionIdSearched); 
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede obtener los puntos turisticos que se estan buscando por dichas categorias y region.", e);
            }
        }

        public TouristSpot UpdateTouristSpot(TouristSpot touristSpot)
        {
            try
            {
                TouristSpot touristSpotDb = touristSpotRepository.Get(touristSpot.Id);
                touristSpotDb.UpdateAttributes(touristSpot);
                touristSpotDb.VerifyFormat(); 
                touristSpotRepository.Update(touristSpotDb);
                return touristSpotDb;
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede actualizar el punto turistico.", e);
            }
        }

        public void RemoveTouristSpot(Guid touristSpotId)
        {
            try
            {
                TouristSpot touristSpotToDelete = touristSpotRepository.Get(touristSpotId);
                touristSpotRepository.Remove(touristSpotToDelete);
            }catch(ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede eliminar el punto turistico deseado.", e); 
            }
        }

        public List<TouristSpot> GetAllTouristSpot()
        {
            try
            {
                List<TouristSpot> allTouristSpot = touristSpotRepository.GetAll().ToList();
                return allTouristSpot; 
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se pudieron obtener todos los puntos turisticos", e);
            }
        }
    }
}
