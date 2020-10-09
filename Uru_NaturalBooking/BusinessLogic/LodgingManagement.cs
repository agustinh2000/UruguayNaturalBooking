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
        private readonly ILodgingRepository lodgingRepository;
        private readonly ITouristSpotManagement touristSpotManagementLogic;

        public LodgingManagement(ILodgingRepository repository, ITouristSpotManagement touristSpotLogic)
        {
            lodgingRepository = repository;
            touristSpotManagementLogic = touristSpotLogic; 
        }

        public LodgingManagement(ILodgingRepository repository)
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
                return lodging;
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede crear el hospedaje debido a que ha ocurrido un error.", e);
            }
        }

        public Lodging GetLodgingById(Guid lodgingId)
        {
            try
            {
                Lodging lodgingObteined = lodgingRepository.Get(lodgingId);
                return lodgingObteined; 
            }catch(ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede obtener el hospedaje deseado", e); 
            }
        }

        public List<Lodging> GetAvailableLodgingsByTouristSpot(Guid touristSpotId)
        {
            try
            {
                return lodgingRepository.GetAvailableLodgingsByTouristSpot(touristSpotId);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede obtener el hospedaje deseado", e);
            }
        }

        public void RemoveLodging(Guid lodgingId)
        {
            try
            {
                Lodging lodgingToDelete = lodgingRepository.Get(lodgingId);
                lodgingRepository.Remove(lodgingToDelete);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede eliminar el hospedaje deseado.", e);
            }
        }

        public Lodging UpdateLodging(Guid aIdOfLodging, Lodging aLodging)
        {
            try
            {
                Lodging lodgingDb = lodgingRepository.Get(aIdOfLodging);
                if (lodgingDb != null)
                {
                    lodgingDb.UpdateAttributes(aLodging);
                    lodgingDb.VerifyFormat(); 
                    lodgingRepository.Update(lodgingDb);
                    return lodgingDb;
                }
                else
                {
                    throw new ServerBusinessLogicException("El hospedaje buscado no existe");
                }
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede actualizar el hospedaje.", e);
            }
        }

        public List<Lodging> GetAllLoadings()
        {
            try
            {
                List<Lodging> allLodgings = lodgingRepository.GetAll().ToList();
                return allLodgings;
            }
            catch(ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotExistLodgigns, e); 
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedAllLodgings, e);
            }
        }
    }
}
