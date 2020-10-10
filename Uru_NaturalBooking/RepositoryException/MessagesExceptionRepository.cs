using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryException
{
    public class MessagesExceptionRepository
    {
        public static string ErrorGetAllElements = "Error. No se pueden obtener los elementos deseados.";
        public static string ErrorGetElementById = "Error. No se pudo obtener el elemento deseado";
        public static string ErrorObteinedTouristSpotByRegionId = "Error. No se puede obtener los puntos turisticos por el Id de region buscado.";
        public static string ErrorGettingTouristSpotByRegionId = "Error. Ha ocurrido un error al tratar de obtener los puntos turisticos buscados por el Id de region.";
        public static string ErrorObteinedTouristSpotByCategories = "Error. No se puede obtener los puntos turisticos por las categorias buscadas.";
        public static string ErrorGettingTouristSpotByCategories = "Error. Ha ocurrido un error al tratar de obtener los puntos turisticos buscados por las categorias."; 
        public static string ErrorObteinedTouristSpotByCategoriesAndRegion = "Error. No se puede obtener los puntos turisticos por las categorias buscadas y la region buscada.";
        public static string ErrorGettingTouristSpotByCategoriesAndRegion = "Error. Ha ocurrido un error al tratar de obtener los puntos turisticos buscados por las categorias y region.";
        public static string ErrorEmailOrPasswordIncorrect = "Error. Email y/o contrasena incorrecto";
        public static string ErrorCheckingEmailAndPassword = "Error. Ha ocurrido un error al verificar el Email y/o la contrasena";
    
        public static string ErrorObteinedAvailableLodgingsByTouristSpotId = "Error. No se puede obtener los hospedajes disponibles según los puntos turisticos buscados.";
        public static string ErrorGettingAvailableLodgingsByTouristSpotId = "Error. Ha ocurrido un error al tratar de obtener los hospedajes disponibles buscados segun el punto turistico."; 
    }
}
