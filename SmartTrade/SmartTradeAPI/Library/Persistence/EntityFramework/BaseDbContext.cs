using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartTrade.Persistence
{
    public class BaseDbContext : DbContext
    {

        /// <summary>
        /// Un-done every change made since last SaveChanges()
        /// </summary>
        public void Rollback()
        {
            // Copiado de: https://www.iteramos.com/pregunta/40487/deshacer-los-cambios-realizados-en-el-marco-de-entidades-de-entidades
            var context = this;
            var changedEntries = context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();
            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }
            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }
            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }
        }
    }


}
