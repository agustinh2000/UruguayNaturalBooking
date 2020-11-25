using Importation;
using ImporterException;
using ImporterJson;
using ImporterXml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ForRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImportationTest
{
    [TestClass]
    public class ReflectionLogicTest
    {
        [TestMethod]
        public void GettingAvailablesImporterOk()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            List<IImport> listOfDllObteined = reflectionLogic.GetAvailableImporters().ToList();
            bool resultOfValuesObteined = listOfDllObteined[0].GetName().Equals("Importador JSON")
                && listOfDllObteined[1].GetName().Equals("Importador XML");
            Assert.IsTrue(resultOfValuesObteined);
        }

        [TestMethod]
        public void GetTheParametersOfJSONDllTestOk()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            List<Parameter> parametersOfImporterJson = reflectionLogic.GetTheParametersRequired( "\\Importers\\ImporterJson.dll");
            JsonImporter importerJson = new JsonImporter();

            CollectionAssert.AreEqual(importerJson.GetParameter(), parametersOfImporterJson);
        }

        [TestMethod]
        public void GetTheParametersOfXmlDllTestOk()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            List<Parameter> parametersOfImporterXml = reflectionLogic.GetTheParametersRequired("\\Importers\\ImporterXml.dll");
            XmlImporter importerXml = new XmlImporter();

            CollectionAssert.AreEqual(importerXml.GetParameter(), parametersOfImporterXml);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FailInGetParametersOfJsonDllWithoutConstructorTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            List<Parameter> parametersOfImporterJson = reflectionLogic.GetTheParametersRequired( "\\ImporterWithoutConstructor\\ImporterJsonWithoutConstructor.dll");
        }

        [TestMethod]
        public void ImportingLodgingsTestOfJsonDllOk()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            List<Parameter> listOfParameters = reflectionLogic.GetTheParametersRequired(  "\\Importers\\ImporterJson.dll");
            listOfParameters[0].Value = Directory.GetCurrentDirectory() + "\\FilesToImport\\Lodgings.json";
            List<LodgingModelForImport> lodgingsImported = reflectionLogic.ImportLodgings(  "\\Importers\\ImporterJson.dll", listOfParameters);

            TouristSpotModelForImport touristSpotModel = new TouristSpotModelForImport()
            {

                Id = new Guid("7cb8a7ab-8511-473f-803f-6b39118e79c1"),
                Name = "Punta del Este",
                Description = "La naturaleza abunda",
                RegionId = new Guid("fc775bb9-8cc8-4fdc-bca6-16e06bcd322b"),
                ImagePath = "Desktop\\pde.jpg",
                ListOfCategoriesId = new List<Guid>() { new Guid("baa98b33-eafe-4d62-bb62-859a8e36d3a9") }
            };

            LodgingModelForImport lodgingModelForImport = new LodgingModelForImport()
            {
                Name = "Hotel Enjoy Conrad",
                Description = "Un lugar magico donde podes vivir.",
                QuantityOfStars = 5,
                Address = "Playa Mansa parada 21",
                Images = new List<string>() { "Desktop\\conrad.jpg" },
                PricePerNight = 200.0,
                IsAvailable = true,
                TouristSpot = touristSpotModel
            };

            Assert.AreEqual(lodgingModelForImport, lodgingsImported[0]);
        }

        [TestMethod]
        public void ImportingLodgingsTestOfXmlDllOk()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfXmlDll =  "\\Importers\\ImporterXml.dll";
            List<Parameter> listOfParameters = reflectionLogic.GetTheParametersRequired(pathOfXmlDll);
            listOfParameters[0].Value = Directory.GetCurrentDirectory() + "\\FilesToImport\\Lodgings.xml";
            List<LodgingModelForImport> lodgingsImported = reflectionLogic.ImportLodgings(pathOfXmlDll, listOfParameters);

            TouristSpotModelForImport touristSpotModel = new TouristSpotModelForImport()
            {
                Id = new Guid("7cb8a7ab-8511-473f-803f-6b39118e79c1"),
                Name = "Punta del Este",
                Description = "La naturaleza abunda",
                RegionId = new Guid("fc775bb9-8cc8-4fdc-bca6-16e06bcd322b"),
                ImagePath = "Desktop\\pde.jpg",
                ListOfCategoriesId = new List<Guid>() { new Guid("baa98b33-eafe-4d62-bb62-859a8e36d3a9") }
            };

            LodgingModelForImport lodgingModelForImport = new LodgingModelForImport()
            {
                Name = "Hotel Enjoy Conrad",
                Description = "Un lugar magico donde podes vivir.",
                QuantityOfStars = 5,
                Address = "Playa Mansa parada 21",
                Images = new List<string>() { "Desktop\\conrad.jpg" },
                PricePerNight = 200.0,
                IsAvailable = true,
                TouristSpot = touristSpotModel
            };

            Assert.AreEqual(lodgingModelForImport, lodgingsImported[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingWithMoreParametersInJsonDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            List<Parameter> listOfParameters = reflectionLogic.GetTheParametersRequired("\\Importers\\ImporterJson.dll");
            listOfParameters[0].Value = Directory.GetCurrentDirectory() + "\\FilesToImport\\Lodgings.json";
            listOfParameters.Add(new Parameter());
            reflectionLogic.ImportLodgings("\\Importers\\ImporterJson.dll", listOfParameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingWithMoreParametersInXmlDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfXmlDll =  "\\Importers\\ImporterXml.dll";
            List<Parameter> listOfParameters = reflectionLogic.GetTheParametersRequired(pathOfXmlDll);
            listOfParameters[0].Value = Directory.GetCurrentDirectory() + "\\FilesToImport\\Lodgings.xml";
            listOfParameters.Add(new Parameter());
            reflectionLogic.ImportLodgings(pathOfXmlDll, listOfParameters);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FailInImportLodgingWithBadDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfDll = "\\ImporterWithoutConstructor\\ImporterJsonWithoutConstructor.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value= Directory.GetCurrentDirectory() + "\\FilesToImport\\Lodgings.json"
                }
            };
            reflectionLogic.ImportLodgings(pathOfDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingWrongPathOfFileJsonDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfDll =  "\\Importers\\ImporterJson.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value= Directory.GetCurrentDirectory() + "\\FilesToImport\\Lodgings"
                }
            };
            reflectionLogic.ImportLodgings(pathOfDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingWrongPathOfFileXmlDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfXmlDll = "\\Importers\\ImporterXml.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfXmlFile",
                    Type= "file",
                    Value= Directory.GetCurrentDirectory() + "\\FilesToImport\\Lodgings"
                }
            };
            reflectionLogic.ImportLodgings(pathOfXmlDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingEmptyPathOfFileJsonDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfDll =  "\\Importers\\ImporterJson.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value= ""
                }
            };
            reflectionLogic.ImportLodgings(pathOfDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingEmptyPathOfFileXmlDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfXmlDll =  "\\Importers\\ImporterXml.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfXmlFile",
                    Type= "file",
                    Value= ""
                }
            };
            reflectionLogic.ImportLodgings(pathOfXmlDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingNullPathOfFileJsonDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfDll =  "\\Importers\\ImporterJson.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value= null
                }
            };
            reflectionLogic.ImportLodgings(pathOfDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingNullPathOfFileXmlDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfXmlDll =  "\\Importers\\ImporterXml.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfXmlFile",
                    Type= "file",
                    Value= null
                }
            };
            reflectionLogic.ImportLodgings(pathOfXmlDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingWrongPathOfDirectoryJSONDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfDll = "\\Importers\\ImporterJson.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value= Directory.GetCurrentDirectory() + "\\FilesToImpor\\Lodgings.json"
                }
            };
            reflectionLogic.ImportLodgings(pathOfDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingWrongPathOfDirectoryXMLDllTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfXmlDll =  "\\Importers\\ImporterXml.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value= Directory.GetCurrentDirectory() + "\\FilesToImpor\\Lodgings.json"
                }
            };
            reflectionLogic.ImportLodgings(pathOfXmlDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingWithErrorInJSONFileTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfDll = "\\Importers\\ImporterJson.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value= Directory.GetCurrentDirectory() + "\\FilesToImport\\LodgingsV2.json"
                }
            };
             reflectionLogic.ImportLodgings(pathOfDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingWithReadErrorInJSONFileTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfDll =  "\\Importers\\ImporterJson.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value= Directory.GetCurrentDirectory() + "\\FilesToImport\\LodgingsV3.json"
                }
            };
             reflectionLogic.ImportLodgings(pathOfDll, listOfParametersExpected);
        }

        [TestMethod]
        [ExpectedException(typeof(ImportationException))]
        public void FailInImportLodgingWithInvalidOperationInXMLFileTest()
        {
            ReflectionLogic reflectionLogic = new ReflectionLogic();
            string pathOfXmlDll =  "\\Importers\\ImporterXml.dll";
            List<Parameter> listOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfXmlFile",
                    Type= "file",
                    Value= Directory.GetCurrentDirectory() + "\\FilesToImport\\LodgingsV2.xml"
                }
            };
            reflectionLogic.ImportLodgings(pathOfXmlDll, listOfParametersExpected);
        }
    }
}
