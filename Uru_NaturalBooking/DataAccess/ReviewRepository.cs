using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        private readonly DbContext context;

        public ReviewRepository(DbContext context) : base(context)
        {
            this.context = context;
        }

        public double GetAverageReviewScoreByLodging(Guid idOfLodging)
        {
            try
            {
                double averageOfReviews = context.Set<Review>().Where(r => r.LodgingOfReview.Id.Equals(idOfLodging)).Average(r => r.Score);
                return averageOfReviews;
            }
            catch (Exception e)
            {
                throw new ServerException(MessagesExceptionRepository.ErrorGettingScoreAverageReview, e);
            }
        }

        public Review GetReviewByReserveId(Guid idOfReserve)
        {
            try
            {
                Review review = context.Set<Review>().Where(r => r.IdOfReserve == idOfReserve).FirstOrDefault();
                return review;
            }
            catch(Exception e)
            {
                throw new ServerException(MessagesExceptionRepository.ErrorGettingReviewByReserveId, e);
            }
        }
    }
}
