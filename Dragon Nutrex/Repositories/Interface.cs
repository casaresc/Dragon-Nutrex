using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();

        T? GetById(Guid id);

        void Create(T entity);

        void Update(T entity);

        void Delete(Guid id);
    }
}
