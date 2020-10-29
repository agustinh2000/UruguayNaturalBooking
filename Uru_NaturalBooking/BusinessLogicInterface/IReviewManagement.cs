using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface IReviewManagement
    {
        Review GetById(Guid idOfReview);
        Review Create(Review review, Guid idOfReserveAssociated);
    }
}
