using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.ForRequest;
using Model.ForResponse;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class RegionControllerTest
    {
        Region region;

        RegionForResponseModel regionModel;

        [TestInitialize]
        public void SetUp()
        {
            region = new Region()
            {
                Id = Guid.NewGuid(),
                Name = Region.RegionName.Región_Centro_Sur
            };

            regionModel = new RegionForResponseModel()
            {
                Id = region.Id,
                Name = Region.RegionName.Región_Centro_Sur,
                DescriptionOfName= "Region Centro Sur"
            };

        }

        [TestMethod]
        public void GetRegionByIdTestOk()
        {
            var regionMock = new Mock<IRegionManagement>(MockBehavior.Strict);
            regionMock.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(region);
            RegionController regionController = new RegionController(regionMock.Object);
            var result = regionController.Get(region.Id);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as RegionForResponseModel;
            regionMock.VerifyAll();
            Assert.AreEqual(regionModel, model);
        }


        [TestMethod]
        public void GetRegionByIdNotFoundTest()
        {
            var regionMock = new Mock<IRegionManagement>(MockBehavior.Strict);
            regionMock.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(value: null) ;
            RegionController regionController = new RegionController(regionMock.Object);
            var result = regionController.Get(region.Id);
            var createdResult = result as NotFoundObjectResult;
            regionMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetRegionByIdInternalErrorTest()
        {
            var regionMock = new Mock<IRegionManagement>(MockBehavior.Strict);
            regionMock.Setup(m => m.GetById(It.IsAny<Guid>())).Throws(new ServerBusinessLogicException("Hubo un error al obtener la region."));
            RegionController regionController = new RegionController(regionMock.Object);
            var result = regionController.Get(region.Id);
            var createdResult = result as ObjectResult;
            regionMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetAllRegionsTestOk()
        {
            var regionMock = new Mock<IRegionManagement>(MockBehavior.Strict);
            regionMock.Setup(m => m.GetAllRegions()).Returns(new List<Region> {region });
            RegionController regionController = new RegionController(regionMock.Object);
            var result = regionController.Get();
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as List <RegionForResponseModel>;
            regionMock.VerifyAll();
            Assert.AreEqual(regionModel, model.First());
        }

        [TestMethod]
        public void GetAllRegionsNotFoundTest()
        {
            var regionMock = new Mock<IRegionManagement>(MockBehavior.Strict);
            regionMock.Setup(m => m.GetAllRegions()).Throws(new ClientBusinessLogicException());
            RegionController regionController = new RegionController(regionMock.Object);
            var result = regionController.Get();
            var createdResult = result as NotFoundObjectResult;
            regionMock.VerifyAll();
            Assert.AreEqual(404, createdResult.StatusCode);
        }

        [TestMethod]
        public void GetAllRegionsInternalErrorTest()
        {
            var regionMock = new Mock<IRegionManagement>(MockBehavior.Strict);
            regionMock.Setup(m => m.GetAllRegions()).Throws(new ServerBusinessLogicException("Hubo un error al obtener la lista de regiones."));
            RegionController regionController = new RegionController(regionMock.Object);
            var result = regionController.Get();
            var createdResult = result as ObjectResult;
            regionMock.VerifyAll();
            Assert.AreEqual(500, createdResult.StatusCode);
        }
    }
}
