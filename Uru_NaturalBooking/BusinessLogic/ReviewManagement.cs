using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using DomainException;
using RepositoryException;
using System;

namespace BusinessLogic
{
    public class ReviewManagement : IReviewManagement
    {
        private readonly IReviewRepository reviewRepository;

        private readonly IReserveManagement reserveManagementLogic;

        private readonly ILodgingManagement lodgingManagementLogic;

        public ReviewManagement(IReviewRepository repository, IReserveManagement reserveLogic, ILodgingManagement lodgingLogic)
        {
            reviewRepository = repository;
            reserveManagementLogic = reserveLogic;
            lodgingManagementLogic = lodgingLogic;
        }

        public ReviewManagement(IReviewRepository repository)
        {
            reviewRepository = repository;
        }

        public Review GetById(Guid idOfReview)
        {
            try
            {
                Review review = reviewRepository.Get(idOfReview);
                return review;
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotFindReview, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedReview, e);
            }
        }

        private void VerifyIfExistReviewForReserve(Guid idOfReserve)
        {
            try
            {
                Review reviewObteined = reviewRepository.GetReviewByReserveId(idOfReserve);
                if (reviewObteined != null)
                {
                    throw new DomainBusinessLogicException(MessageExceptionBusinessLogic.ErrorReviewAlredyExistForThisReserveCode);
                }
            }
            catch (ServerException e)
            {
                throw new ServerException("No se puede crear la reseña debido a que ha ocurrido un error.", e);
            }
        }

        public Review Create(Review review, Guid idOfReserveAssociated)
        {
            try
            {
                Reserve reserveAssociated = reserveManagementLogic.GetById(idOfReserveAssociated);
                VerifyIfExistReviewForReserve(idOfReserveAssociated);
                review.IdOfReserve = idOfReserveAssociated;
                review.NameOfWhoComments = reserveAssociated.Name;
                review.LastNameOfWhoComments = reserveAssociated.LastName;
                Lodging lodgingOfReview = reserveAssociated.LodgingOfReserve;
                review.LodgingOfReview = lodgingOfReview;
                review.VerifyFormat();
                reviewRepository.Add(review);
                double averageReviewScoreUpdated = reviewRepository.GetAverageReviewScoreByLodging(lodgingOfReview.Id);
                lodgingManagementLogic.UpdateAverageReviewScore(lodgingOfReview, averageReviewScoreUpdated);
                return review;
            }
            catch (ReviewException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (ClientBusinessLogicException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorCreatingReview, e);
            }
            catch (DomainBusinessLogicException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede crear la review debido a que ha ocurrido un error.", e);
            }
        }
    }
}
