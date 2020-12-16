using System.Data.Entity;

namespace SQLGit.Patterns.UnitOfWork
{
    public interface IUnitOfWork<out TContext>
        where  TContext : DbContext, new()
    {
        TContext Context { get; }
        void CreateTransaction();
        void Rollback();
        void Commit();
        void Save();
    }
}