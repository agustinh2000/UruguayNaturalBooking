

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
    public class RegionDATest
    {
        [TestMethod]
        public void TestAddRegionOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);

            Region regionToAdd = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            regionRepo.Add(regionToAdd);

            List<Region> listOfRegion = regionRepo.GetAll().ToList();

            Assert.AreEqual(regionToAdd, listOfRegion[0]);
        }

        [TestMethod]
        public void TestGetRegionOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);

            Region regionToAdd = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            regionRepo.Add(regionToAdd);
            Region regionOfDb = regionRepo.Get(regionToAdd.Id);

            Assert.AreEqual(regionToAdd, regionOfDb);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestGetRegionBad()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);

            Region regionToAdd = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            Region regionOfDb = regionRepo.Get(regionToAdd.Id);
        }

        [TestMethod]
        public void TestRemoveRegionOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);

            Region regionToAdd = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            regionRepo.Add(regionToAdd);
            regionRepo.Remove(regionToAdd);

            List<Region> listOfRegion = regionRepo.GetAll().ToList();

            Assert.IsTrue(listOfRegion.IsNullOrEmpty());
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestRemoveRegionInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);

            Region regionToAdd = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            regionRepo.Remove(regionToAdd);
        }

        [TestMethod]
        public void TestUpdateRegionOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);

            Region regionToAdd = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            regionRepo.Add(regionToAdd);
            regionToAdd.Name = Region.RegionName.Región_Corredor_Pajaros_Pintados;
            regionRepo.Update(regionToAdd);

            List<Region> listOfRegions = regionRepo.GetAll().ToList();

            Assert.AreNotEqual(Region.RegionName.Región_Centro_Sur, listOfRegions[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionRepository))]
        public void TestUpdateRegionInvalid()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);

            Region regionToAdd = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };
            regionRepo.Update(regionToAdd);
        }

        [TestMethod]
        public void TestGetAllRegionOK()
        {
            ContextObl context = ContextFactory.GetMemoryContext(Guid.NewGuid().ToString());
            IRepository<Region> regionRepo = new BaseRepository<Region>(context);

            Region regionToAdd = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            Region regionToAdd2 = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Este
            };

            regionRepo.Add(regionToAdd);
            regionRepo.Add(regionToAdd2);

            List<Region> listTest = new List<Region>();
            listTest.Add(regionToAdd);
            listTest.Add(regionToAdd2);

            List<Region> listOfCategories = regionRepo.GetAll().ToList();

            CollectionAssert.AreEqual(listTest, listOfCategories);
        }

    }
}
