using Biblioteca.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Service
{
    public class CrudService<T> : ICrudService<T>
    {
        private readonly IRepository<T> _repository;

        public CrudService(IRepository<T> repository)
        {
            _repository = repository;
        }        

        public void Delete(int? id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.Table.ToList();
        }

        public T GetById(object id)
        {
            return _repository.GetById(id);
        }

        public T Insert(T entity)
        {
            return _repository.Insert(entity);
        }

        public T Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}
