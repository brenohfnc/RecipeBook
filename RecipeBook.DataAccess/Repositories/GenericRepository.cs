using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace RecipeBook.DataAccess
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await Context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetAsync(int ID)
        {
            return await Context.Set<TEntity>().FindAsync(ID);
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int ID)
        {
            TEntity entity = await GetAsync(ID);

            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
