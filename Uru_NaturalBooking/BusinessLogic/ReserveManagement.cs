using BusinessLogicException;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using DomainException;
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
                Lodging lodgingOfReserve = lodgingManagement.GetLodgingById(lodgingId);
                reserve.LodgingOfReserve = lodgingOfReserve;
                reserve.StateOfReserve = Reserve.ReserveState.Creada;
                reserve.VerifyFormat();
                reserveRepository.Add(reserve);
                return reserve;
            }
            catch (ReserveException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (ClientBusinessLogicException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorCreatingReserve, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException("No se puede crear la reserva deseada.", e);
            }
        }

        public Reserve GetById(Guid idOfReserve)
        {
            try
            {
                Reserve reserve = reserveRepository.Get(idOfReserve);
                return reserve;
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorNotFindReserve, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorObteinedReserve, e);
            }
        }

        public Reserve Update(Guid id, Reserve aReserve)
        {
            try
            {
                Reserve reserveOfDb = reserveRepository.Get(id);
                reserveOfDb.UpdateAttributes(aReserve);
                reserveOfDb.VerifyFormat();
                reserveRepository.Update(reserveOfDb);
                return reserveOfDb;
            }
            catch (ReserveException e)
            {
                throw new DomainBusinessLogicException(e.Message);
            }
            catch (ClientException e)
            {
                throw new ClientBusinessLogicException(MessageExceptionBusinessLogic.ErrorUpdatingReserveNotFound, e);
            }
            catch (ServerException e)
            {
                throw new ServerBusinessLogicException(MessageExceptionBusinessLogic.ErrorUpdatingReserve, e);
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
