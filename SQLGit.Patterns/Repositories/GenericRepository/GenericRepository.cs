using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using SQLGit.Patterns.Models;
using SQLGit.Patterns.UnitOfWork;

namespace SQLGit.Patterns.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private IDbSet<T> _entities;
        private string _error = String.Empty;
        private bool _disposed;

        public GenericRepository(IUnitOfWork<DBContext> unitOfWork)
            : this(unitOfWork.Context)
        {
        }

        public GenericRepository(DBContext context)
        {
            Context = context;
        }

        public DBContext Context { get; set; }

        public virtual IQueryable<T> Tables
        {
            get { return Entities; }
        }

        public virtual IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = Context.Set<T>()); }
        }
        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }

            _disposed = true;
        }

        public IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual void Insert(T obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException("obj");
                }
                
                if (Context == null || _disposed)
                {
                    Context = new DBContext();
                }
                Entities.Add(obj);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _error += string.Format("Property: {0} Error: {1}", validationError.PropertyName,
                                      validationError.ErrorMessage)
                                  + Environment.NewLine;
                    }
                }
                throw new Exception(_error, e);
            }
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentException("entities");
                }

                Context.Configuration.AutoDetectChangesEnabled = false;
                Context.Set<T>().AddRange(entities);
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _error += string.Format("Property: {0} Error: {1}", validationError.PropertyName,
                                      validationError.ErrorMessage)
                                  + Environment.NewLine;
                    }
                }

                throw new Exception(_error, e);
            }
        }
        
        public virtual void Update(T obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException("obj");
                }
                if (Context == null || _disposed)
                {
                    Context = new DBContext();
                }

                SetEntryModified(obj);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _error += string.Format("Property: {0} Error: {1}", validationError.PropertyName,
                                      validationError.PropertyName)
                                  + Environment.NewLine;
                    }
                }

                throw new Exception(_error, e);
            }
        }

        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
        

        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
        
                if (Context == null || _disposed)
                {
                    Context = new DBContext();
                }
        
                Entities.Remove(entity);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _error += string.Format("Property: {0} Error: {1}", validationError.PropertyName,
                                      validationError.ErrorMessage)
                                  + Environment.NewLine;
                    }
                }
        
                throw new Exception(_error, e);
            }
        }
    }
}