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
                if (listOfLodgingInTouristSpot.IsNullOrEmpty())
                {
                    throw new ClientException("No hay hospedajes disponibles para el punto turistico indicado.");
                }
                return listOfLodgingInTouristSpot;
            }
            catch (ClientException e)
            {
                throw new ClientException(MessagesExceptionRepository.ErrorObteinedAvailableLodgingsByTouristSpotId, e);
            }
            catch (Exception e)
            {
                throw new ServerException(MessagesExceptionRepository.ErrorGettingAvailableLodgingsByTouristSpotId, e);
            }
        }

        public Lodging GetLodgingByNameAndTouristSpot(string lodgingName, Guid touristSpotId)
        {
            try
            {
                Lodging lodgingObteined = context.Set<Lodging>().Where(x => x.Name.Equals(lodgingName) && x.TouristSpot.Id.Equals(touristSpotId)).FirstOrDefault();
                return lodgingObteined;
            }
            catch (Exception e)
            {
                throw new ServerException(MessagesExceptionRepository.ErrorObteinedLodgingByNameAndTouristSpotId, e);
            }
        }

        public List<Lodging> GetLodgingsWithReserves(Guid idOfTouristSpot, DateTime dateCheckInMax, DateTime dateCheckOutMax)
        {
            try
            {
                List<Lodging> listOfLodgingsWithReserves = context.Set<Lodging>().AsEnumerable().Where(x => x.TouristSpot.Id.Equals(idOfTouristSpot)
                && x.QuantityOfReserveForThePeriod(dateCheckInMax, dateCheckOutMax) > 0)
                    .OrderByDescending(x => x.QuantityOfReserveForThePeriod(dateCheckInMax, dateCheckOutMax))
                    .ThenBy(x => x.CreationDate).ToList();

                if (listOfLodgingsWithReserves.IsNullOrEmpty())
                {
                    throw new ClientException("No se pudo obtener los hospedajes con reservas."); 
                }
                return listOfLodgingsWithReserves; 

            }catch(ClientException e)
            {
                throw new ClientException(MessagesExceptionRepository.ErrorGettingLodgingWithReserves, e); 
            }
            catch (Exception e)
            {
                throw new ServerException(MessagesExceptionRepository.ErrorObteinedLodgingWithReserves, e); 
            }
        }
    }
}
