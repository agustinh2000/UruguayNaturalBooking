using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class CategoryManagement : ICategoryManagement
    {
        private IRepository<Category> categoryRepository;

        public CategoryManagement(IRepository<Category> repository)
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
            catch(ClientException e)
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
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotFindUser, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedUser, e);
            }
        }

        public List<Category> GetAssociatedCategories(List<Guid> categoriesId)
        {
            List<Category> listOfCategoriesToAssociated = new List<Category>(); 
            
             foreach(Guid identifierCategory in categoriesId)
             {
                    Category category = GetById(identifierCategory);
                    listOfCategoriesToAssociated.Add(category); 
             }
             return listOfCategoriesToAssociated;   
        }

        public Category Create(Category categoryToCreate)
        {
            try
            {
                categoryToCreate.Id = Guid.NewGuid();
                categoryToCreate.VerifyFormat();
                categoryRepository.Add(categoryToCreate);
                return categoryToCreate; 

            }catch(ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede crear la categoria deseada", e); 
            }
        }
    }
}
