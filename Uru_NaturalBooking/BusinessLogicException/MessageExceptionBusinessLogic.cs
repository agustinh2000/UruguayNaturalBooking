using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicException
{
    public class MessageExceptionBusinessLogic
    {
        public static string ErrorObteinedAllCategories = "Error. Ocurrio un error al intentar obtener todas las categorias.";
        public static string ErrorNotExistCategories = "Error. No se han encontrado categorias.";
        public static string ErrorObteinedAllRegion = "Error. Ocurrio un error al intentar obtener todas las regiones.";
        public static string ErrorNotExistRegion = "Error. No se han encontrado regiones.";
        public static string ErrorObteinedAllLodgings = "Error. Ocurrio un error al intentar obtener todos los hospedajes.";
        public static string ErrorNotExistLodgigns = "Error. No se han encontrado hospedajes.";
        public static string ErrorObteinedAllTouristSpots = "Error. Ocurrio un error al intentar obtener todos los hospedajes.";
        public static string ErrorNotExistTouristSpots = "Error. No se han encontrado puntos turisticos.";
        public static string ErrorObteinedAllUser = "Error. Ocurrio un error al intentar obtener todos los usuarios.";
        public static string ErrorNotExistUsers = "Error. No se han encontrado usuarios.";
        public static string ErrorNotFindUser = "Error. No se ha encontrado el usuario buscado.";
        public static string ErrorObteinedUser = "Error. Ha ocurrido un error al intentar encontrar el usuario buscado.";
        public static string ErrorNotFindLodging = "Error. No se ha encontrado el hospedaje buscado.";
        public static string ErrorObteinedLodging = "Error. Ha ocurrido un error al intentar encontrar el hospedaje buscado.";
        public static string ErrorNotFindRegion = "Error. No se ha encontrado la region buscada.";
        public static string ErrorObteinedRegion = "Error. Ha ocurrido un error al intentar encontrar la region buscada.";
        public static string ErrorNotFindReserve = "Error. No se ha encontrado la reserva buscada.";
        public static string ErrorObteinedReserve = "Error. Ha ocurrido un error al intentar encontrar la reserva buscada.";
        public static string ErrorNotFindTouristSpot = "Error. No se ha encontrado el punto turistico buscado.";
        public static string ErrorObteinedTouristSpot = "Error. Ha ocurrido un error al intentar encontrar el punto turistico buscado.";
        public static string ErrorNotFindReview = "Error. No se ha encontrado la review buscada.";
        public static string ErrorObteinedReview = "Error. Ha ocurrido un error al intentar encontrar la review buscada.";
        public static string ErrorCreatingLodging = "Error. No se ha podido encontrar el punto turistico asociado al hospedaje a crear.";
        public static string ErrorCreatingReserve = "Error. No se ha podido encontrar el hospedaje asociado a la reserva a crear.";
        public static string ErrorCreatingReview = "Error. No se ha podido encontrar la reserva asociada a la review a crear.";
        public static string ErrorCreatingTouristSpot = "Error. No se ha podido encontrar la region y/o las categorias asociadas al punto turistico.";
        public static string ErrorUpdatingReserve = "Error. No se ha podido actualizar la reserva.";
        public static string ErrorUpdatingReserveNotFound = "Error. No se ha podido encontrar la reserva que se desea actualizar";
        public static string ErrorObteinedTouristSpotByCategories = "Error. No se puede obtener los puntos turisticos por las categorias buscadas.";
        public static string ErrorGettingTouristSpotByCategories = "Error. Ha ocurrido un error al tratar de obtener los puntos turisticos buscados por las categorias.";
        public static string ErrorObteinedTouristSpotByCategoriesAndRegion = "Error. No se puede obtener los puntos turisticos por las categorias buscadas y la region buscada.";
        public static string ErrorGettingTouristSpotByCategoriesAndRegion = "Error. Ha ocurrido un error al tratar de obtener los puntos turisticos buscados por las categorias y region.";
        public static string ErrorUserAlredyExist = "Error. El mail del usuario que desea crear ya existe.";
        public static string ErrorReviewAlredyExistForThisReserveCode = "Error. Ya ha ingresado una reseña para el codigo de reserva dado.";
        public static string ErrorTouristSpotAlredyExist = "Error. El punto turistico que desea crear ya existe.";
        public static string ErrorLodgingAlredyExist = "Error. El hospedaje que desea crear ya existe.";
        public static string ErrorCategoryAlredyExist = "Error. La categoria que desea crear ya existe.";
        public static string ErrorNotFindCategory = "Error. No se ha encontrado la categoria buscada.";
        public static string ErrorGettingCategory = "Error. Ha ocurrido un error al intentar obtener la categoria buscada.";
        public static string ErrorGettingLodgingWithReserves = "Error. No se pudo obtener hospedajes con reservas.";
        public static string ErrorObteinedLodgingWithReserves = "Error. Ha ocurrido un error al tratar de obtener los hospedajes con reservas.";
    }
}
