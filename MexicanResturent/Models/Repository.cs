


using MexicanResturent.Data;
using Microsoft.EntityFrameworkCore;

namespace MexicanResturent.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }
        private DbSet<T> _dbset;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           T entity=await _dbset.FindAsync(id);
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _dbset.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, QueryOptions<T> options)
        {
            IQueryable<T> query = _dbset;

            if (options.HasWhere)
            {
                query=query.Where(options.Where);
            }
            if (options.HasOrderBy)
            {
                query=query.OrderBy(options.OrderBy);
            }
            foreach(string include in options.GetIncludes())
            {
                query=query.Include(include);
            }

            var key =_context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.FirstOrDefault();
            string primaryKeyName = key?.Name;
            return await query.FirstOrDefaultAsync(e=>EF.Property<int>(e,primaryKeyName)==id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllByIdAsync<TKey>(TKey id, string propertyName, QueryOptions<T> options)
        {
            IQueryable<T> query = _dbset;

            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }

            if (options.HasOrderBy)
            {
                query=query.OrderBy(options.OrderBy);
            }

            foreach (string include in options.GetIncludes())
            {
                query=query.Include(include);
            }
            // filter

            query = query.Where(e =>EF.Property<TKey>(e, propertyName).Equals(id));

            return await query.ToListAsync();
        }
    }
}
