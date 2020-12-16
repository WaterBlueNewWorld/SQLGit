using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using SQLGit.Patterns.Repositories.GenericRepository;

namespace SQLGit.Patterns.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable
        where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private bool _disposed;
        private string _error = String.Empty;
        private DbContextTransaction _objTransaction;
        private Dictionary<string, object> _repositories;

        public UnitOfWork()
        {
            _context = new TContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public TContext Context
        {
            get { return _context; }
        }

        public void Commit()
        {
            _objTransaction.Commit();
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validation in e.EntityValidationErrors)
                {
                    foreach (var valError in validation.ValidationErrors)
                    {
                        _error += string.Format("Property: {0} Error {1}",
                            valError.PropertyName, valError.ErrorMessage) + Environment.NewLine;
                        throw new Exception(_error, e);
                    }
                }
            }
        }

        public void Rollback()
        {
            _objTransaction.Rollback();
        }

        public void CreateTransaction()
        {
            _objTransaction = _context.Database.BeginTransaction();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public GenericRepository<T> GenericRepository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, object>();

            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repoType = typeof(GenericRepository<T>);
                var repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repoInstance);
            }

            return (GenericRepository<T>) _repositories[type];
        }
    }
}