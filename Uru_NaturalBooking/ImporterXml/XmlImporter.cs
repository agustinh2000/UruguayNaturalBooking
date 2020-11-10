using Importation;
using ImporterException;
using Model.ForRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ImporterXml
{
    public class XmlImporter : IImport
    {
        public string Name { get; set; }

        public List<Parameter> ListOfParametersExpected { get; set; }

        public XmlImporter()
        {
            Name = "Importador XML"; 
            ListOfParametersExpected = new List<Parameter>()
            {
                new Parameter()
                {
                    Name= "pathOfXmlFile",
                    Type= "file",
                    Value= ""
                }
            };
        }

        public XmlImporter(List<Parameter> listOfParametersExpected)
        {
            Name = "Importador XML";
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
                string xmlContent = File.ReadAllText(ListOfParametersExpected[0].Value);

                XmlSerializer serializer = new XmlSerializer(typeof(List<LodgingModelForImport>), new XmlRootAttribute("Lodgings"));

                StringReader stringReader = new StringReader(xmlContent);

                List<LodgingModelForImport> lodgingsImported = (List<LodgingModelForImport>)serializer.Deserialize(stringReader);

                return lodgingsImported;
            }
            catch (FileNotFoundException e)
            {
                throw new ImportationException(MessagesExceptionImporter.FileNotFound, e);
            }
            catch(DirectoryNotFoundException e)
            {
                throw new ImportationException(MessagesExceptionImporter.DirectoryNotFound, e); 
            }
            catch (InvalidOperationException e)
            {
                throw new ImportationException(MessagesExceptionImporter.FormatError, e);
            }
            catch(ArgumentException e)
            {
                throw new ImportationException(MessagesExceptionImporter.ArgumentError, e); 
            }
            catch (Exception e)
            {
                throw new ImportationException(MessagesExceptionImporter.ErrorUnexpected, e);
            }
        }
    }
}
