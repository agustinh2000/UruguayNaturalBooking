using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using DomainException;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class LodgingManagementForImportation : ILodgingManagementForImportation
    {
        private readonly ILodgingRepository lodgingRepository;
        private readonly ITouristSpotManagement touristSpotManagementLogic;

        public LodgingManagementForImportation(ILodgingRepository repository, ITouristSpotManagement touristSpotLogic)
        {
            lodgingRepository = repository;
            touristSpotManagementLogic = touristSpotLogic;
        }


        public Lodging Create(Lodging lodging, TouristSpot touristSpot, List<string> pathOfPictures)
        {
            try
            {
                Lodging lodgingObteined = lodgingRepository.GetLodgingByNameAndTouristSpot(lodging.Name, touristSpot.Id);
                if (lodgingObteined != null)
                {
                    throw new DomainBusinessLogicException(MessageExceptionBusinessLogic.ErrorLodgingAlredyExist);
                }
                lodging.Id = Guid.NewGuid();
                TouristSpot touristSpotForLodging; 
                try
                {
                    touristSpotForLodging = touristSpotManagementLogic.GetTouristSpotById(touristSpot.Id);
                }
                catch (ClientBusinessLogicException e)
                {
                    touristSpotForLodging = touristSpotManagementLogic.Create(touristSpot, touristSpot.Region.Id, touristSpot.ListOfCategories.ConvertAll(c => c.CategoryId)); 
                }

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
    }
}
