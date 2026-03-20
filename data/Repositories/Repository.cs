using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using domain.Interface;
using data.Context;

namespace data.Repositories
{


 
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly ProjectContext Context;

            public Repository(ProjectContext context)
            {
            Context = context;
            }

            #region Async Functions

            public virtual async Task AddAsync(T entity)
            {
                try
                {
                    await Context.Set<T>().AddAsync(entity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            public virtual async Task AddRangeAsync(IEnumerable<T> entities)
            {
                try
                {
                    await Context.Set<T>().AddRangeAsync(entities);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            public virtual async Task<T> GetAsync(Expression<Func<T, bool>> condition = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
            {
                try
                {
                    IQueryable<T> query = Context.Set<T>();
                    if (includes != null)
                        query = includes(query);

                    return condition != null
                        ? await query.FirstOrDefaultAsync(condition)
                        : await query.FirstOrDefaultAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            public virtual async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> condition = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
            {
                IQueryable<T> query = Context.Set<T>();
                if (includes != null)
                    query = includes(query);

                return condition != null
                    ? await query.Where(condition).ToListAsync()
                    : await query.ToListAsync();
            }

        #endregion

        #region Sync Functions

        //public string Add(T entity)
        //{
        //    try
        //    {
        //        Context.Set<T>().Add(entity);
        //        Context.SaveChanges();
        //        return "Added successfully";
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return $"Error while adding: {e.Message}";
        //    }
        //}
        public string Add(T entity)
        {
            try
            {
                Context.Set<T>().Add(entity);
                Context.SaveChanges();
                return "Added successfully";
            }
            catch (DbUpdateException dbEx)
            {
                // Parcours toute la chaîne d'inner exceptions
                Exception inner = dbEx.InnerException;
                string innerMessages = "";
                while (inner != null)
                {
                    innerMessages += inner.Message + " | ";
                    inner = inner.InnerException;
                }

                return $"Error while adding: {dbEx.Message}. Inner exceptions: {innerMessages}";
            }
            catch (Exception e)
            {
                return $"Error while adding: {e.Message}";
            }
        }


        public T Get(Expression<Func<T, bool>> condition = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
            {
                IQueryable<T> query = Context.Set<T>();
                if (includes != null)
                    query = includes(query);

                return condition != null ? query.FirstOrDefault(condition) : query.FirstOrDefault();
            }

            public IEnumerable<T> GetList(Expression<Func<T, bool>> condition = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
            {
                IQueryable<T> query = Context.Set<T>();
                if (includes != null)
                    query = includes(query);

                return condition != null ? query.Where(condition).ToList() : query.ToList();
            }

            public string Update(T entity)
            {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
                return "Updated successfully";
            }

            public string Remove(Guid id)
            {
                var entity = Context.Set<T>().Find(id);
                if (entity != null)
                {
                Context.Set<T>().Remove(entity);
                Context.SaveChanges();
                    return "Deleted successfully";
                }
                return "Entity not found";
            }

        public string Removeobject(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ExecuteStoreQueryAsync(string commandText, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ExecuteStoreQueryAsync(string commandText, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ExecuteStoreQuery(string commandText, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ExecuteStoreQuery(string commandText, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> entites)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    }


