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
    public class LodgingRepository : BaseRepository<Lodging>, ILodgingRepository
    {
        private readonly DbContext context;

        public LodgingRepository(DbContext context) : base(context)
        {
            this.context = context;
        }
        public List<Lodging> GetAvailableLodgingsByTouristSpot(Guid touristSpotId)
        {
            try
            {
                List<Lodging> listOfLodgingInTouristSpot = context.Set<Lodging>().Where(lod => lod.TouristSpot.Id.Equals(touristSpotId)
                                && lod.IsAvailable).ToList();
                if(listOfLodgingInTouristSpot.IsNullOrEmpty())
                {
                    throw new ServerException("No hay hospedajes disponibles para el punto turistico indicado.");
                }
                return listOfLodgingInTouristSpot;
            }
            catch(Exception e)
            {
                throw new ServerException(e.Message, e);

            }
        }
    }
}
