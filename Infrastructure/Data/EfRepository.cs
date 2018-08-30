using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<T> ListAllQuery()
        {
            return _dbContext.Set<T>().AsQueryable<T>();
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public T GetSingleBySpec(ISpecification<T> spec)
        {
            return List(spec).FirstOrDefault();
        }


        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public IEnumerable<T> List(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Criteria)
                            .AsEnumerable();
        }
        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return await secondaryResult
                            .Where(spec.Criteria)
                            .ToListAsync();
        }

        public virtual T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }
        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}