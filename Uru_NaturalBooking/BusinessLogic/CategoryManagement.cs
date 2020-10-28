using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using DomainException;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class CategoryManagement : ICategoryManagement
    {
        private ICategoryRepository categoryRepository;

        public CategoryManagement(ICategoryRepository repository)
        {
            categoryRepository = repository;
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                List<Category> allCategories = categoryRepository.GetAll().ToList();
                return allCategories;
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotExistCategories, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedAllCategories, e);
            }
        }

        public Category GetById(Guid identifier)
        {
            try
            {
                Category category = categoryRepository.Get(identifier);
                return category;
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotFindCategory, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorGettingCategory, e);
            }
        }

        public List<Category> GetAssociatedCategories(List<Guid> categoriesId)
        {
            List<Category> listOfCategoriesToAssociated = new List<Category>();
            if (categoriesId != null)
            {
                foreach (Guid identifierCategory in categoriesId)
                {
                    Category category = GetById(identifierCategory);
                    listOfCategoriesToAssociated.Add(category);
                }
            }
            return listOfCategoriesToAssociated;
        }

        public Category Create(Category categoryToCreate)
        {
            try
            {
                VerifyIfCategoryExist(categoryToCreate);
                categoryToCreate.Id = Guid.NewGuid();
                categoryToCreate.VerifyFormat();
                categoryRepository.Add(categoryToCreate);
                return categoryToCreate;
            }
            catch (CategoryException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (DomainBusinessLogicException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede crear la categoria deseada", e);
            }
        }

        private void VerifyIfCategoryExist(Category category)
        {
            try
            {
                Category catgegoryObteined = categoryRepository.GetCategoryByName(category.Name);
                if (catgegoryObteined != null)
                {
                    throw new DomainBusinessLogicException(MessageExceptionBusinessLogic.ErrorCategoryAlredyExist);
                }
            }
            catch (ServerException e)
            {
                throw new ServerException("No se puede crear la categoria debido a que ha ocurrido un error.", e);
            }
        }

    }
}
