using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessInterface
{
    public interface ICategoryRepository : IRepository<Category>
    {

        Category GetCategoryByName(string categoryName);
    }
}
