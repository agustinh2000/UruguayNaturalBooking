using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

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
            catch(ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotFindRegion, e);

            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedRegion, e);
            }

        }

        public List<Region> GetAllRegions()
        {
            try
            {
                List<Region> allRegions = regionRepository.GetAll().ToList();
                return allRegions;
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotExistRegion, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedAllRegion, e);
            }
        }

    }
}
