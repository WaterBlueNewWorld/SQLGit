using System.Collections.Generic;

namespace SQLGit.GenericRepository
{
    public interface IGenericRepository <T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Insert(object id);
        void Save();

    }
}