using System;
using System.Collections.Generic;
using System.Text;

namespace ImporterException
{
    public class MessagesExceptionImporter
    {
        public static string DirectoryNotFound = "Error. No se ha encontrado el directorio en la ruta especificada. "; 
        public static string FileNotFound = "Error. No se ha encontrado el archivo a leer el contenido.";
        public static string SerializationError = "Ha ocurrido un error durante la serializacion.";
        public static string ReadFileError = "Ha ocurrido un error al intentar leer el texto JSON.";
        public static string ErrorUnexpected = "Ha ocurrido un error inesperado al intentar leer el archivo.";
        public static string ErrorQuantityParams = "La cantidad de parametros recibida es distinta a la cantidad de parametros esperada";
        public static string FormatError = "Error. El archivo se encuentra mal formado, por favor verifiquelo. ";
        public static string ArgumentError = "Error. El argumento no puede ser vacio/nulo";
    }
}
