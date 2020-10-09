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
        public static string ErrorQuantity = "Error. La cantidad de estrellas debe estar entre 1 y 5.";
        public static string ErrorPrice = "Error. El precio por noche del hospedaje debe estar entre 0 y 100000.";
        public static string ErrorDoesNotHaveCategory = "Error. El punto turistico debe tener al menos una categoria asociada";
        public static string ErrorDate = "Error. La fecha de CheckIn no puede ser despues de la fecha de CheckOut";
        public static string ErrorQuantityOfGuest = "Error. La cantidad total de huespedes debe ser mayor a cero. ";
        public static string ErrorEmail = "Error. El mail ingresado es invalido";
        public static string ErrorTokenNotExist = "Error. El token no existe";
        public static string ErrorInvalidEmail = "Error. El email es invalido.";
        public static string ErrorPicture = "Error. El path de la foto no debe ser vacio."; 
    }
}
