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
        public void GetRegionValidByIdTestOk()
        {
            Region region = new Region
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };
            var regionMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(region);

            RegionManagement regionLogic = new RegionManagement(regionMock.Object);

            Region result = regionLogic.GetById(region.Id);

            Region regiontToCompare = new Region()
            {
                Id = result.Id,
                Name = result.Name
            };

            regionMock.VerifyAll();
            Assert.AreEqual(region, regiontToCompare);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void GetInvalidRegionByIdTest()
        {
            var regionId = Guid.NewGuid();
            Region region = new Region
            {
                Id = regionId,
                Name = Region.RegionName.Región_Centro_Sur
            };

            var regionMock = new Mock<IRepository<Region>>(MockBehavior.Strict);

            regionMock.Setup(x => x.Get(It.IsAny<Guid>())).Throws(new ServerException());

            RegionManagement regionLogic = new RegionManagement(regionMock.Object);

            regionLogic.GetById(regionId);
        }

        [TestMethod]
        public void GetAllRegionTestOk()
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


            var regionMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionMock.Setup(m => m.GetAll()).Returns(listOfRegions);

            RegionManagement regionLogic = new RegionManagement(regionMock.Object);

            List<Region> result = regionLogic.GetAllRegions();

            regionMock.VerifyAll();
            CollectionAssert.AreEqual(listOfRegions, result); 
        }


        [TestMethod]
        [ExpectedException(typeof(ServerBusinessLogicException))]
        public void GetBadAllRegionTestOk()
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


            var regionMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionMock.Setup(m => m.GetAll()).Throws(new ServerException());

            RegionManagement regionLogic = new RegionManagement(regionMock.Object);

            List<Region> result = regionLogic.GetAllRegions();
        }

        [TestMethod]
        [ExpectedException(typeof(ClientBusinessLogicException))]
        public void GetClientExceptionGettingAllRegionTest()
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


            var regionMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            regionMock.Setup(m => m.GetAll()).Throws(new ClientException());

            RegionManagement regionLogic = new RegionManagement(regionMock.Object);

            List<Region> result = regionLogic.GetAllRegions();
        }

    }
}
