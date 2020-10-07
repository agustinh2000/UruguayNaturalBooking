using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic
{
    public class ReserveManagement : IReserveManagement
    {
        private readonly IRepository<Reserve> reserveRepository;
        private readonly ILodgingManagement lodgingManagement;

        public ReserveManagement(IRepository<Reserve> repository, ILodgingManagement lodgingLogic)
        {
            reserveRepository = repository;
            lodgingManagement = lodgingLogic;
        }

        public ReserveManagement(IRepository<Reserve> repository)
        {
            reserveRepository = repository; 
        }

        public Reserve Create(Reserve reserve, Guid lodgingId)
        {
            try
            {
                reserve.Id = Guid.NewGuid();
                reserve.PhoneNumberOfContact = Int32.Parse(RandomPhoneNumber(8)); 
                reserve.DescriptionForGuest = RandomDescription(50);
                reserve.LodgingOfReserve = lodgingManagement.GetLodgingById(lodgingId);
                reserve.StateOfReserve = Reserve.ReserveState.Creada; 
                reserve.VerifyFormat();
                reserveRepository.Add(reserve);
                reserveRepository.Save(); 
                return reserve; 

            }catch(ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("No se puede crear la reserva deseada.", e); 
            }
        }

        public Reserve GetById(Guid idOfReserve)
        {
            try
            {
                Reserve reserve = reserveRepository.Get(idOfReserve);
                return reserve;
            }
            catch (ExceptionRepository e)
            {
                throw new ExceptionBusinessLogic("Hubo un error al obtener la reserva deseada.", e);
            }
        }

        private string RandomDescription(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string RandomPhoneNumber(int length)
        {
            Random random = new Random();
            string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
