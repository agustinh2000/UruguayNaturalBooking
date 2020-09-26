using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class RegionManagement : IRegionManagement
    {
        private IRepository<Region> regionRepository;

        public RegionManagement(IRepository<Region> repository)
        {
            regionRepository = repository;
        }

        public Region GetById(Guid identifier)
        {
            try
            {
                Region region = regionRepository.Get(identifier);
                return region;
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("Hubo un error al obtener la region.", e);
            }

        }

        public List<Region> GetAllRegions()
        {
            try
            {
                List<Region> allRegions = regionRepository.GetAll().ToList();
                return allRegions; 
            }
            catch(ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("Hubo un error al obtener la lista de regiones", e); 
            }
        }

    }
}
