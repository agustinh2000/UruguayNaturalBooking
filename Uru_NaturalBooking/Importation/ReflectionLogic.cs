using Domain;
using ImporterException;
using Model.ForRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Importation
{
    public class ReflectionLogic
    {
        public string PathOfWhereAreImporters { get; set; }

        public ReflectionLogic() {
            PathOfWhereAreImporters = Directory.GetCurrentDirectory() + "\\Importers";
        }

        public IEnumerable<IImport> GetAvailableImporters()
        {
            string[] filesPath = Directory.GetFiles(PathOfWhereAreImporters, "*.dll");

            foreach (string file in filesPath)
            {
                Assembly dll = Assembly.UnsafeLoadFrom(file);

                IEnumerable<Type> types = dll.GetTypes().Where(i => typeof(IImport).IsAssignableFrom(i));
                foreach (Type type in types)
                {
                    yield return Activator.CreateInstance(type) as IImport;
                }
            }
        }

        public List<Parameter> GetTheParametersRequired(string importerPath)
        {
            List<Parameter> listOfParametersRequired = new List<Parameter>();
            Assembly dll = Assembly.UnsafeLoadFrom(importerPath);

            IEnumerable<Type> types = dll.GetTypes().Where(i => typeof(IImport).IsAssignableFrom(i));
            foreach (Type type in types)
            {
                try
                {
                    IImport instanceOfImport = Activator.CreateInstance(type) as IImport;
                    listOfParametersRequired = instanceOfImport.GetParameter();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return listOfParametersRequired;
        }

        public List<LodgingModelForImport> ImportLodgings(string importerPath, List<Parameter> parameterValues)
        {
            List<LodgingModelForImport> lodgingToImport = new List<LodgingModelForImport>();
            Assembly dll = Assembly.UnsafeLoadFrom(importerPath);

            IEnumerable<Type> types = dll.GetTypes().Where(i => typeof(IImport).IsAssignableFrom(i));
            foreach (Type type in types)
            {
                IImport instanceOfImport;
                int countOfParametersExpected;
                try
                {
                    countOfParametersExpected = (Activator.CreateInstance(type) as IImport).GetParameter().Count;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                if (parameterValues.Count != countOfParametersExpected)
                {
                    throw new ImportationException(MessagesExceptionImporter.ErrorQuantityParams);
                }
                else
                {
                        instanceOfImport = Activator.CreateInstance(type, parameterValues) as IImport;
                }

                lodgingToImport = instanceOfImport.Import();
            }
            return lodgingToImport;
        }

    }
}
