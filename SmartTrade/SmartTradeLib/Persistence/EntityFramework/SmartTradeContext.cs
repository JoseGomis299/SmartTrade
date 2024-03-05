using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace SmartTradeLib.Persistence;

public class SmartTradeContext : BaseDbContext
{

    public SmartTradeContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=tcp:furgoneta.database.windows.net,1433;Initial Catalog=SmartTradeDB;Persist Security Info=False;User ID=CloudSAf9df27ed;Password=123456789Aa;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


    /// <summary>
    /// Delete every object stored in the DbSets
    /// Every class in the Namespace "...Entities" must have a DbSet in the DbContext
    /// </summary>
    public virtual void RemoveAllData()
    {
        foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (!t.FullName.Contains(".Entities")) continue;
            if (t.BaseType.FullName == "System.Enum") continue;
            if (t.IsNested) continue;

            var dbSetProperties = GetType().GetProperties().Where(p => p.PropertyType.GetGenericArguments().FirstOrDefault() == t);

            foreach (var dbSetProperty in dbSetProperties)
            {
                var dbSet = dbSetProperty.GetValue(this) as IQueryable<object>;
                if (dbSet != null)
                {
                    var entitiesToRemove = dbSet.ToList();
                    RemoveRange(entitiesToRemove);
                }
            }
        }

        SaveChanges();
    }
}