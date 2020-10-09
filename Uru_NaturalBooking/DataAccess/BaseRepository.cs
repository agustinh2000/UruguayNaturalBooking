using Castle.Core.Internal;
using DataAccessInterface;
using Microsoft.EntityFrameworkCore;
using RepositoryException;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; set; }

        public BaseRepository(DbContext aContext)
        {
            Context = aContext;
        }

        public void Add(T entity)
        {
            try
            {
                Context.Set<T>().Add(entity);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ServerException(e.Message, e);
            }
        }

        public void Remove(T entity)

        {
            try
            {
                Context.Set<T>().Remove(entity);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ServerException(e.Message, e);
            }
        }

        public void Update(T entity)
        {
            try
            {
                Context.Set<T>().Update(entity);
                Context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new ServerException(e.Message, e);
            }
        }

        public T Get(Guid id)
        {
            try
            {
                T entityObteined = Context.Set<T>().Find(id);
                if (entityObteined == null)
                {
                    throw new ClientException();
                }
                return entityObteined;
            }
            catch (ClientException e)
            {
                throw new ClientException(MessagesExceptionRepository.ErrorGetElementById, e);
            }
            catch (Exception e)
            {
                throw new ServerException(e.Message, e);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                List<T> listOfEntities = Context.Set<T>().ToList();
                if (listOfEntities.IsNullOrEmpty())
                {
                    throw new ClientException();
                }
                return listOfEntities;
            }
            catch (ClientException e)
            {
                throw new ClientException(MessagesExceptionRepository.ErrorGetAllElements, e);
            }
            catch (Exception e)
            {
                throw new ServerException(e.Message, e);
            }
        }
    }
}
