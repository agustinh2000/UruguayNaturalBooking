using Castle.Core.Internal;
using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class TouristSpotRepository : BaseRepository<TouristSpot>, ITouristSpotRepository
    {
        private readonly DbContext context;

        public TouristSpotRepository(DbContext context) : base(context)
        {
            this.context = context;
        }

        public List<TouristSpot> GetTouristSpotsByCategoriesAndRegion(List<Guid> listOfCategoriesIdSearched, Guid regionIdSearched)
        {
            try
            {
                List<TouristSpot> listOfTouristSpot = new List<TouristSpot>();
                    listOfTouristSpot = context.Set<TouristSpot>().Include(d => d.ListOfCategories).Include(d => d.Region).Include(d => d.Image).AsEnumerable().Where(m =>
                    m.Region.Id.Equals(regionIdSearched) && m.HasCategoriesSearched(listOfCategoriesIdSearched)).ToList();
                if (listOfTouristSpot.IsNullOrEmpty())
                {
                    throw new ClientException("Error obteniendo los elementos deseados");
                }
                return listOfTouristSpot;
            }
            catch (ClientException e)
            {
                throw new ClientException(MessagesExceptionRepository.ErrorObteinedTouristSpotByCategoriesAndRegion, e);
            }
            catch (Exception e)
            {
                throw new ServerException(MessagesExceptionRepository.ErrorGettingTouristSpotByCategoriesAndRegion, e);
            }
        }

        public TouristSpot GetTouristSpotByName(string touristSpotName)
        {
            try
            {
                TouristSpot touristSpotObteined = context.Set<TouristSpot>().Where(x => x.Name.Equals(touristSpotName)).FirstOrDefault();
                return touristSpotObteined;
            }
            catch (Exception e)
            {
                throw new ServerException(MessagesExceptionRepository.ErrorObteinedTouristSpotByName, e);
            }
        }
    }
}
