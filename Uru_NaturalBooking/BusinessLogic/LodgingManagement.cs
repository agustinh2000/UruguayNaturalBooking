using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using DomainException;
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

        public Lodging Create(Lodging lodging, Guid touristSpotId, List<string> pathOfPictures)
        {
            try
            {
                VerifyIfLodgingExist(lodging, touristSpotId);
                lodging.Id = Guid.NewGuid();
                TouristSpot touristSpotForLodging = touristSpotManagementLogic.GetTouristSpotById(touristSpotId);
                lodging.TouristSpot = touristSpotForLodging;

                if (pathOfPictures != null)
                {
                    foreach (string picturePath in pathOfPictures)
                    {
                        Picture pictureOfLodging = new Picture()
                        {
                            Path = picturePath,
                            Id = Guid.NewGuid()
                        };

                        LodgingPicture lodgingPicture = new LodgingPicture()
                        {
                            Lodging = lodging,
                            LodgingId = lodging.Id,
                            Picture = pictureOfLodging,
                            PictureId = pictureOfLodging.Id
                        };
                        lodging.Images.Add(lodgingPicture);
                    }
                }
                lodging.VerifyFormat();
                lodgingRepository.Add(lodging);
                return lodging;
            }
            catch (LodgingException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (DomainBusinessLogicException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (ClientBusinessLogicException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorCreatingLodging, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede crear el hospedaje debido a que ha ocurrido un error.", e);
            }
        }

        private void VerifyIfLodgingExist(Lodging lodging, Guid touristSpotId)
        {
            try
            {
                Lodging lodgingObteined = lodgingRepository.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpotId);
                if (lodgingObteined != null)
                {
                    throw new DomainBusinessLogicException(MessageExceptionBusinessLogic.ErrorLodgingAlredyExist);
                }
            }
            catch (ServerException e)
            {
                throw new ServerException("No se puede crear el hospedaje debido a que ha ocurrido un error.", e);
            }
        }


        public Lodging GetLodgingById(Guid lodgingId)
        {
            try
            {
                Lodging lodgingObteined = lodgingRepository.Get(lodgingId);
                return lodgingObteined;
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotFindLodging, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedLodging, e);
            }
        }

        public List<Lodging> GetAvailableLodgingsByTouristSpot(Guid touristSpotId)
        {
            try
            {
                return lodgingRepository.GetAvailableLodgingsByTouristSpot(touristSpotId);
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(e.Message, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(e.Message, e);
            }
        }

        public void RemoveLodging(Guid lodgingId)
        {
            try
            {
                Lodging lodgingToDelete = lodgingRepository.Get(lodgingId);
                lodgingRepository.Remove(lodgingToDelete);
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotFindLodging, e);
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
                Lodging lodgingDb = GetLodgingById(aIdOfLodging);
                lodgingDb.UpdateAttributes(aLodging);
                lodgingDb.VerifyFormat();
                lodgingRepository.Update(lodgingDb);
                return lodgingDb;
            }
            catch (LodgingException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (ClientBusinessLogicException e)
            {
                throw new ClientBusinessLogicException(e.Message);
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
            catch (ClientException e)
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
