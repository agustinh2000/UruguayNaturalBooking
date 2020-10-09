using Castle.Core.Internal;
using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class TouristSpotRepository : BaseRepository<TouristSpot>, ITouristSpotRepository
    {
        private readonly DbContext context;

        public TouristSpotRepository(DbContext context) : base(context)
        {
            this.context = context;
        }

        public List<TouristSpot> GetTouristSpotByRegion(Guid regionId)
        {
            try
            {
                List<TouristSpot> listOfTouristSpot= context.Set<TouristSpot>().Where(m => m.Region.Id.Equals(regionId)).ToList();
                if (listOfTouristSpot.IsNullOrEmpty())
                {
                    throw new ServerException("Error obteniendo los elementos deseados");
                }
                return listOfTouristSpot;
            }
            catch (Exception e)
            {
                throw new ServerException("No se puede obtener el punto turistico por la region.", e);
            }
        }

        public List<TouristSpot> GetTouristSpotsByCategories(List<Guid> listOfCategoriesIdSearched)
        {
            try
            {
                List<TouristSpot> listOfTouristSpot = context.Set<TouristSpot>().AsEnumerable().Where(m => m.HasCategoriesSearched(listOfCategoriesIdSearched)).ToList();
                if (listOfTouristSpot.IsNullOrEmpty())
                {
                    throw new ServerException("Error obteniendo los elementos deseados");
                }
                return listOfTouristSpot;
            }
            catch (Exception e)
            {
                throw new ServerException("No se puede obtener los puntos turisticos que se estan buscando por dichas categorias.", e);
            }
        }

        public List<TouristSpot> GetTouristSpotsByCategoriesAndRegion(List<Guid> listOfCategoriesIdSearched, Guid regionIdSearched)
        {
            try
            {
                List<TouristSpot> listOfTouristSpot = context.Set<TouristSpot>().AsEnumerable().Where(m => m.Region.Id.Equals(regionIdSearched) 
                && m.HasCategoriesSearched(listOfCategoriesIdSearched)).ToList();
                if (listOfTouristSpot.IsNullOrEmpty())
                {
                    throw new ServerException("Error obteniendo los elementos deseados");
                }
                return listOfTouristSpot;
            }
            catch (Exception e)
            {
                throw new ServerException("No se puede obtener los puntos turisticos que se estan buscando por dichas categorias y region.", e);
            }
        }

    }
}
