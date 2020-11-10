using Domain;
using Importation;
using ImporterException;
using Model.ForRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace ImporterJson
{
    public class JsonImporter : IImport
    {
        public string Name { get; set; }

        public List<Parameter> ListOfParametersExpected { get; set; }

        public JsonImporter()
        {
            Name = "Importador JSON";
            ListOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfJsonFile",
                    Type= "file",
                    Value= ""
                }
            };
        }

        public JsonImporter(List<Parameter> listOfParametersExpected)
        {
            Name = "Importador JSON";
            ListOfParametersExpected = listOfParametersExpected; 
        }

        public string GetName()
        {
            return Name; 
        }

        public List<Parameter> GetParameter()
        {
            return ListOfParametersExpected; 
        }

        public List<LodgingModelForImport> Import()
        {
            try
            {
                string jsonFileContent = File.ReadAllText(ListOfParametersExpected[0].Value);
                List<LodgingModelForImport> lodgingsImported = JsonConvert.DeserializeObject<List<LodgingModelForImport>>(jsonFileContent);
                return lodgingsImported; 
            }
            catch(FileNotFoundException e)
            {
                throw new ImportationException(MessagesExceptionImporter.FileNotFound, e); 
            }
            catch(DirectoryNotFoundException e)
            {
                throw new ImportationException(MessagesExceptionImporter.DirectoryNotFound, e); 
            }
            catch(JsonSerializationException e)
            {
                throw new ImportationException(MessagesExceptionImporter.SerializationError, e); 
            }
            catch(JsonReaderException e)
            {
                throw new ImportationException(MessagesExceptionImporter.ReadFileError, e); 
            }
            catch(ArgumentException e)
            {
                throw new ImportationException(MessagesExceptionImporter.ArgumentError, e); 
            }catch(Exception e)
            {
                throw new ImportationException(MessagesExceptionImporter.ErrorUnexpected, e);
            }
        }
    }
}
