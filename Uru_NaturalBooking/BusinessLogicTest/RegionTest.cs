using BusinessLogic;
using BusinessLogicException;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryException;
using System;
using System.Collections.Generic;

namespace BusinessLogicTest
{
    [TestClass]
    public class RegionTest
    {
        [TestMethod]
        public void GetRegionById()
        {
            Region region = new Region
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };
            var mock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(region.Id)).Returns(region);

            var regionLogic = new RegionManagement(mock.Object);

            var result = regionLogic.GetById(region.Id);

            mock.VerifyAll();
            Assert.AreEqual(region.Name.ToString(), result.Name.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetInvalidRegionById()
        {
            var guid = Guid.NewGuid();
            Region region = new Region
            {
                Id = guid,
                Name = Region.RegionName.Región_Centro_Sur
            };

            var mock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            
            mock.Setup(x => x.Get(guid)).Throws(new ExceptionRepository());

            var regionLogic = new RegionManagement(mock.Object);

            regionLogic.GetById(guid);
        }

        [TestMethod]
        public void GetAllRegion()
        {

            List<Region> listOfRegions = new List<Region>();
            Region region = new Region
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };
            listOfRegions.Add(region);
            Region region2 = new Region
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Corredor_Pajaros_Pintados
            };
            listOfRegions.Add(region2); 


            var mock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Returns(listOfRegions);

            var regionLogic = new RegionManagement(mock.Object);

            var result = regionLogic.GetAllRegions();

            mock.VerifyAll();
            CollectionAssert.AreEqual(listOfRegions, result); 
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionBusinessLogic))]
        public void GetBadAllRegion()
        {

            List<Region> listOfRegions = new List<Region>();
            Region region = new Region
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };
            listOfRegions.Add(region);
            Region region2 = new Region
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Corredor_Pajaros_Pintados
            };
            listOfRegions.Add(region2);


            var mock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll()).Throws(new ExceptionRepository());

            var regionLogic = new RegionManagement(mock.Object);

            var result = regionLogic.GetAllRegions();

            mock.VerifyAll();
        }


    }
}
