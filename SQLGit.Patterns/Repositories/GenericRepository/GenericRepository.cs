using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}