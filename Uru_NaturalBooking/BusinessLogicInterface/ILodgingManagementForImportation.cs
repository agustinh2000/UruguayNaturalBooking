using Domain;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface ILodgingManagementForImportation
    {
        Lodging Create(Lodging lodging, TouristSpot touristSpotId, List<string> pathOfPictures);
    }
}
