using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class LodgingManagement : ILodgingManagement
    {
        private readonly IRepository<Lodging> lodgingRepository;
        private readonly ITouristSpotManagement touristSpotManagementLogic;

        public LodgingManagement(IRepository<Lodging> repository, ITouristSpotManagement touristSpotLogic)
        {
            lodgingRepository = repository;
            touristSpotManagementLogic = touristSpotLogic; 
        }

        public LodgingManagement(IRepository<Lodging> repository)
        {
            lodgingRepository = repository; 
        }

        public Lodging Create(Lodging lodging, Guid touristSpotId)
        {
            try
            {
                lodging.Id = Guid.NewGuid();
                TouristSpot touristSpotForLodging = touristSpotManagementLogic.GetTouristSpotById(touristSpotId);
                lodging.TouristSpot = touristSpotForLodging; 
                lodging.VerifyFormat();
                lodgingRepository.Add(lodging);
                lodgingRepository.Save();
                return lodging;
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede crear el hospedaje debido a que ha ocurrido un error.", e);
            }
        }

        public Lodging GetLodgingById(Guid lodgingId)
        {
            try
            {
                Lodging lodgingObteined = lodgingRepository.Get(lodgingId);
                return lodgingObteined; 
            }catch(ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede obtener el hospedaje deseado", e); 
            }
        }

        public List<Lodging> GetLodgingsByTouristSpot(Guid touristSpotId)
        {
            try
            {
                List<Lodging> listOfLodgingInTouristSpot = lodgingRepository.GetAll().Where(lod => lod.TouristSpot.Id.Equals(touristSpotId)).ToList();
                return listOfLodgingInTouristSpot; 
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede obtener el hospedaje deseado", e);
            }
        }
    }
}
