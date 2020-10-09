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
                throw new ExceptionRepository(e.Message, e);
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
                throw new ExceptionRepository(e.Message, e);
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

                throw new ExceptionRepository(e.Message, e);
            }
        }

        public T Get(Guid id)
        {
            try
            {
                T entityObteined =  Context.Set<T>().Find(id);
                if(entityObteined == null)
                {
                    throw new ExceptionRepository("Error obteniendo el elemento deseado"); 
                }
                return entityObteined; 
            }
            catch (Exception e)
            {
                throw new ExceptionRepository(e.Message, e);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return Context.Set<T>().ToList();
            }
            catch (Exception e)
            {
                throw new ExceptionRepository(e.Message, e);
            }
        } 
    }
}
