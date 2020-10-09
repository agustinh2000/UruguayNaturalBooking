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
            List<Category> listOfCategories = categoryRepo.GetAll().ToList();

            Assert.AreEqual(categoryToAdd, listOfCategories[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestAddCategoryNullInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);
            categoryRepo.Add(null);
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
            Category categoryOfDb = categoryRepo.Get(categoryToAdd.Id); 

            Assert.AreEqual(categoryToAdd, categoryOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
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
        [ExpectedException(typeof(ClientException))]
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
            categoryRepo.Remove(categoryToAdd);

            categoryRepo.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
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
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestRemoveCategoryNullInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);
            categoryRepo.Remove(null);
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
            categoryToAdd.Name = "Piscinas"; 
            categoryRepo.Update(categoryToAdd);

            List<Category> listOfCategories = categoryRepo.GetAll().ToList();

            Assert.AreNotEqual("Playa", listOfCategories[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
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
        }

        [TestMethod]
        [ExpectedException(typeof(ServerException))]
        public void TestUpdateCategoryNullInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Category> categoryRepo = new BaseRepository<Category>(context);
            categoryRepo.Update(null);
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

            List<Category> listTest = new List<Category>();
            listTest.Add(categoryToAdd);
            listTest.Add(categoryToAdd2); 

            List<Category> listOfCategories = categoryRepo.GetAll().ToList();

            CollectionAssert.AreEqual(listTest, listOfCategories);
        }

    }
}
