using Castle.Core.Internal;
using DataAccess;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAcessTest
{
    [TestClass]
    public class CategoryDATest
    {
        [TestMethod]
        public void TestAddCategoryOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);

            Category categoryToAdd = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            categoryRepo.Add(categoryToAdd);
            categoryRepo.Save(); 
            
            List<Category> listOfCategories = categoryRepo.GetAll().ToList();

            Assert.AreEqual(categoryToAdd, listOfCategories[0]);
        }

        [TestMethod]
        public void TestGetCategoryOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);

            Category categoryToAdd = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            categoryRepo.Add(categoryToAdd);
            categoryRepo.Save();
            Category categoryOfDb = categoryRepo.Get(categoryToAdd.Id); 

            Assert.AreEqual(categoryToAdd, categoryOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestGetCategoryBad()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);

            Category categoryToAdd = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };
            
            Category categoryOfDb = categoryRepo.Get(categoryToAdd.Id);
        }


        [TestMethod]
        public void TestRemoveCategoryOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);

            Category categoryToAdd = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            categoryRepo.Add(categoryToAdd);
            categoryRepo.Save();
            categoryRepo.Remove(categoryToAdd);
            categoryRepo.Save(); 

            List<Category> listOfCategories = categoryRepo.GetAll().ToList();

            Assert.IsTrue(listOfCategories.IsNullOrEmpty()); 
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestRemoveCategoryInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);

            Category categoryToAdd = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            categoryRepo.Remove(categoryToAdd);
            categoryRepo.Save();
        }


        [TestMethod]
        public void TestUpdateCategoryOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);

            Category categoryToAdd = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            categoryRepo.Add(categoryToAdd);
            categoryRepo.Save();

            categoryToAdd.Name = "Piscinas"; 

            categoryRepo.Update(categoryToAdd);
            categoryRepo.Save();

            List<Category> listOfCategories = categoryRepo.GetAll().ToList();

            Assert.AreNotEqual("Playa", listOfCategories[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestUpdateCategoryInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);

            Category categoryToAdd = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            categoryRepo.Update(categoryToAdd);
            categoryRepo.Save();
        }


        [TestMethod]
        public void TestGetAllCategoriesOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);

            Category categoryToAdd = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Playa"
            };

            Category categoryToAdd2 = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Piscina"
            };

            categoryRepo.Add(categoryToAdd);
            categoryRepo.Add(categoryToAdd2); 
            categoryRepo.Save();

            List<Category> listTest = new List<Category>();
            listTest.Add(categoryToAdd);
            listTest.Add(categoryToAdd2); 

            List<Category> listOfCategories = categoryRepo.GetAll().ToList();

            CollectionAssert.AreEqual(listTest, listOfCategories);
        }

    }
}
