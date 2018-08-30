using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        T GetSingleBySpec(ISpecification<T> spec);
        IEnumerable<T> ListAll();
        IQueryable<T> ListAllQuery();
        IEnumerable<T> List(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}