using AzureFuncRestApi.Data;
using AzureFuncRestApi.Interfaces;
using AzureFuncRestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureFuncRestApi.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseModel
    {
        private readonly AppDbContext dbContext;

        public GenericService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.Set<T>().FindAsync(id);
            if (entity == null) throw new Exception($"{id} not found");

            dbContext.Set<T>().Remove(entity);
            int result = await dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var dbEntity = await dbContext.Set<T>().FindAsync(id);

            if (dbEntity == null) throw new Exception($"{id} not found");

            dbContext.Set<T>().Update(entity);
            
            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
