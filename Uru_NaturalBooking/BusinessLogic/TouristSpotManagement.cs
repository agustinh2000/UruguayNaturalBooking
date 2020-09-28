﻿using BusinessLogicException;
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
        private readonly IRepository<TouristSpot> touristSpotRepository;
        private readonly IRegionManagement regionManagementLogic;
        private readonly ICategoryManagement categoryManagementLogic;

        public TouristSpotManagement(IRepository<TouristSpot> repository, IRegionManagement regionLogic, ICategoryManagement categoryLogic)
        {
            touristSpotRepository = repository;
            regionManagementLogic = regionLogic;
            categoryManagementLogic = categoryLogic;
        }

        public TouristSpotManagement(IRepository<TouristSpot> repository)
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
                touristSpotRepository.Save();
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
                List<TouristSpot> listOfTouristSpot = new List<TouristSpot>();
                listOfTouristSpot = touristSpotRepository.GetAll().Where(m => m.Region.Id.Equals(regionId)).ToList();
                return listOfTouristSpot;
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
                List<CategoryTouristSpot> list = new List<CategoryTouristSpot>();

                for (int i = 0; i < listOfCategoriesIdSearched.Count; i++)
                {
                    CategoryTouristSpot categoryTouristSpot = new CategoryTouristSpot()
                    {
                        CategoryId = listOfCategoriesIdSearched[i]
                    };
                    list.Add(categoryTouristSpot);
                }

                List<TouristSpot> listOfTouristSpot = touristSpotRepository.GetAll().Where(m => m.ListOfCategories.SequenceEqual(list)).ToList();
                return listOfTouristSpot;
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
                List<CategoryTouristSpot> list = new List<CategoryTouristSpot>();

                for (int i = 0; i < listOfCategoriesIdSearched.Count; i++)
                {
                    CategoryTouristSpot categoryTouristSpot = new CategoryTouristSpot()
                    {
                        CategoryId = listOfCategoriesIdSearched[i]
                    };
                    list.Add(categoryTouristSpot);
                }

                List<TouristSpot> listOfTouristSpot = touristSpotRepository.GetAll()
                    .Where(m => m.ListOfCategories.SequenceEqual(list) && m.Region.Id.Equals(regionIdSearched)).ToList();
                return listOfTouristSpot;
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
                touristSpotRepository.Save();
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
                touristSpotRepository.Save();
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