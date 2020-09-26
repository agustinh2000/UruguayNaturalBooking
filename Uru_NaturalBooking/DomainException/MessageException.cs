using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class MessageException
    {
        public static string ErrorIsEmpty = "Error. El texto no puede ser vacio.";
        public static string ErrorIsLongerThanTheLimit = "Error. El texto no puede ser superior de 2000 caracteres.";
        public static string ErrorNotHasCategories = "Error. No tiene categorias definidas. ";
    }
}
