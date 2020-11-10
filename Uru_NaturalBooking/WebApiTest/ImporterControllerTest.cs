using BusinessLogicInterface;
using Domain;
using Importation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ForResponse;
using Moq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class ImporterControllerTest
    { 

        [TestMethod]
        public void GetImportersTestOk()
        {
            var lodgingManagementForImportationMock = new Mock<ILodgingManagementForImportation>(MockBehavior.Strict);
            ReflectionLogic reflectionLogic = new ReflectionLogic(); 

            ImporterController importerController = new ImporterController(lodgingManagementForImportationMock.Object, reflectionLogic);

            var result = importerController.Get(); 
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as List<string>;

            lodgingManagementForImportationMock.VerifyAll();
            Assert.IsTrue(model[0].Equals("Importador JSON")
                && model[1].Equals("Importador XML"));
        }

        [TestMethod]
        public void GetFailInGetImportersTest()
        {
            var lodgingManagementForImportationMock = new Mock<ILodgingManagementForImportation>(MockBehavior.Strict);
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            reflectionLogic.PathOfWhereAreImporters = Directory.GetCurrentDirectory() + "\\FilesToImport"; 

            ImporterController importerController = new ImporterController(lodgingManagementForImportationMock.Object, reflectionLogic);

            var result = importerController.Get();
            var createdResult = result as NotFoundObjectResult;

            lodgingManagementForImportationMock.VerifyAll();

            Assert.AreEqual(404, createdResult.StatusCode); 
        }

        [TestMethod]
        public void GetParametersOfJsonDllOkTest()
        {
            var lodgingManagementForImportationMock = new Mock<ILodgingManagementForImportation>(MockBehavior.Strict);
            ReflectionLogic reflectionLogic = new ReflectionLogic();

            ImporterController importerController = new ImporterController(lodgingManagementForImportationMock.Object, reflectionLogic);

            string sourcePathJson = Directory.GetCurrentDirectory() + "\\Importers\\ImporterJson.dll";

            var result = importerController.GetParameters(sourcePathJson);
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as List<Parameter>;

            lodgingManagementForImportationMock.VerifyAll();
           
            Assert.IsTrue(model[0].Name.Equals("pathOfJsonFile") && model[0].Type.Equals("file"));
        }

        [TestMethod]
        public void GetParametersOfJsonDllExceptionTest()
        {
            var lodgingManagementForImportationMock = new Mock<ILodgingManagementForImportation>(MockBehavior.Strict);
            ReflectionLogic reflectionLogic = new ReflectionLogic();

            ImporterController importerController = new ImporterController(lodgingManagementForImportationMock.Object, reflectionLogic);

            string sourcePathJson = Directory.GetCurrentDirectory() + "\\Importers\\ImporterDrive";

            var result = importerController.GetParameters(sourcePathJson);
            var createdResult = result as ObjectResult;

            lodgingManagementForImportationMock.VerifyAll();

            Assert.AreEqual(500, createdResult.StatusCode);
        }

        [TestMethod]
        public void TestPostOfOk()
        {
            var lodgingManagementForImportationMock = new Mock<ILodgingManagementForImportation>(MockBehavior.Strict);
            lodgingManagementForImportationMock.Setup(m => m.Create(It.IsAny<Lodging>(), It.IsAny<TouristSpot>(), It.IsAny<List<string>>())).Returns(It.IsAny<Lodging>());
            ReflectionLogic reflectionLogic = new ReflectionLogic();

            ImporterController importerController = new ImporterController(lodgingManagementForImportationMock.Object, reflectionLogic);

            string sourcePathJson = Directory.GetCurrentDirectory() + "\\Importers\\ImporterJson.dll";

            List<Parameter> listOfParameters = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value=  Directory.GetCurrentDirectory() + "\\FilesToImport\\Lodgings.json"
                }
            };

            var result = importerController.Post(listOfParameters, sourcePathJson); 
            var createdResult = result as OkObjectResult;
            var model = createdResult.Value as string; 

            lodgingManagementForImportationMock.VerifyAll();

            Assert.AreEqual("Todos los hospedajes fueron agregados al sistema.", model);
        }

        [TestMethod]
        public void FailInTestOfPostLodgings()
        {
            var lodgingManagementForImportationMock = new Mock<ILodgingManagementForImportation>(MockBehavior.Strict);
            lodgingManagementForImportationMock.Setup(m => m.Create(It.IsAny<Lodging>(), It.IsAny<TouristSpot>(), It.IsAny<List<string>>())).Throws(new Exception());
            ReflectionLogic reflectionLogic = new ReflectionLogic();

            ImporterController importerController = new ImporterController(lodgingManagementForImportationMock.Object, reflectionLogic);

            string sourcePathJson = Directory.GetCurrentDirectory() + "\\Importers\\ImporterJson.dll";

            List<Parameter> listOfParameters = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value=  Directory.GetCurrentDirectory() + "\\FilesToImport\\Lodgings.json"
                }
            };

            var result = importerController.Post(listOfParameters, sourcePathJson);
            var createdResult = result as BadRequestObjectResult;

            lodgingManagementForImportationMock.VerifyAll();

            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void FailInTestOfPostLodgingsBecauseHaveWrongFile()
        {
            var lodgingManagementForImportationMock = new Mock<ILodgingManagementForImportation>(MockBehavior.Strict);
            ReflectionLogic reflectionLogic = new ReflectionLogic();

            ImporterController importerController = new ImporterController(lodgingManagementForImportationMock.Object, reflectionLogic);

            string sourcePathJson = Directory.GetCurrentDirectory() + "\\Importers\\ImporterJson.dll";

            List<Parameter> listOfParameters = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value=  Directory.GetCurrentDirectory() + "\\FilesToImport\\LodgingsV2.json"
                }
            };

            var result = importerController.Post(listOfParameters, sourcePathJson);
            var createdResult = result as BadRequestObjectResult; 
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void FailInTestOfPostLodgingsBecauseHaveEmptyFile()
        {
            var lodgingManagementForImportationMock = new Mock<ILodgingManagementForImportation>(MockBehavior.Strict);
            ReflectionLogic reflectionLogic = new ReflectionLogic();

            ImporterController importerController = new ImporterController(lodgingManagementForImportationMock.Object, reflectionLogic);

            string sourcePathJson = Directory.GetCurrentDirectory() + "\\Importers\\ImporterJson.dll";

            List<Parameter> listOfParameters = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value=  Directory.GetCurrentDirectory() + "\\FilesToImport\\LodgingsV4.json"
                }
            };

            var result = importerController.Post(listOfParameters, sourcePathJson);
            var createdResult = result as BadRequestObjectResult;
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void FailInTestOfPostLodgingsBecauseHaveAExceptionImportingLodgings()
        {
            var lodgingManagementForImportationMock = new Mock<ILodgingManagementForImportation>(MockBehavior.Strict);
            ReflectionLogic reflectionLogic = new ReflectionLogic();

            ImporterController importerController = new ImporterController(lodgingManagementForImportationMock.Object, reflectionLogic);

            string sourcePathJson = Directory.GetCurrentDirectory() + "\\Importers\\ImporterJsonWithoutConstructor.dll";

            List<Parameter> listOfParameters = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value=  Directory.GetCurrentDirectory() + "\\FilesToImport\\LodgingsV4.json"
                }
            };

            var result = importerController.Post(listOfParameters, sourcePathJson);
            var createdResult = result as ObjectResult;
            Assert.AreEqual(500, createdResult.StatusCode);
        }
    }
}
