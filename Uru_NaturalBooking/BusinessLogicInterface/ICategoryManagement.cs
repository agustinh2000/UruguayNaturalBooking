using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface ICategoryManagement
    {
        Category Create(Category category); 

        Category GetById(Guid identifier);

        List<Category> GetAllCategories();

        List<Category> GetAssociatedCategories(List<Guid> categoriesId); 

    }
}
