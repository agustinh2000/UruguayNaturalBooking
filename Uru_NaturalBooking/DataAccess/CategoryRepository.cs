using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly DbContext context;

        public CategoryRepository(DbContext context) : base(context)
        {
            this.context = context;
        }

        public Category GetCategoryByName(string categoryName)
        {
            try
            {
                Category categoryObteined = context.Set<Category>().Where(x => x.Name.Equals(categoryName)).FirstOrDefault();
                return categoryObteined;
            }
            catch (Exception e)
            {
                throw new ServerException(MessagesExceptionRepository.ErrorObteinedCategoryByName, e);
            }
        }
    }
}
