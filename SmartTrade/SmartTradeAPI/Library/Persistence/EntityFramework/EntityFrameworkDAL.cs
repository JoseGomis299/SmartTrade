using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SmartTrade.Persistence
{
    public class EntityFrameworkDAL : IDAL
    {
        private readonly SmartTradeContext dbContext;

        public EntityFrameworkDAL(SmartTradeContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Insert<T>(T entity) where T : class
        {
            dbContext.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            dbContext.Set<T>().Remove(entity);
        }

        public DbSet<T> GetAll<T>() where T : class
        {
            return dbContext.Set<T>();
        }

        public T GetById<T>(IComparable id) where T : class
        {
            return dbContext.Set<T>().Find(id);
        }

        public bool Exists<T>(IComparable id) where T : class
        {
            return dbContext.Set<T>().Find(id) != null;
        }

        public void Clear<T>() where T : class
        {
            dbContext.Set<T>().RemoveRange(dbContext.Set<T>());
        }

        public IEnumerable<T> GetWhere<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbContext.Set<T>().Where(predicate).AsEnumerable();
        }

        public async void CommitAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public void Rollback()
        {
            dbContext.Rollback();
        }

        public void RemoveAllData()
        {
            dbContext.RemoveAllData();
        }
    }
}
