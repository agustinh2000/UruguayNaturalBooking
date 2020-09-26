using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface IRegionManagement
    {
        Region GetById(Guid identifier);

        List<Region> GetAllRegions(); 
    }
}
