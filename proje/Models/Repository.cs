using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using proje.Utility;

namespace proje.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        internal DbSet<T> dbSet; // dbSet =_appDbContext.SurveyTypes

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            this.dbSet = _appDbContext.Set<T>();
            _appDbContext.Surveys.Include(k => k.SurveyType).Include(k => k.SurveyTypeId);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> query = dbSet;

            if(!string.IsNullOrEmpty(includeProps))
            {
                foreach(var includeProp in includeProps.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }
    }
}
