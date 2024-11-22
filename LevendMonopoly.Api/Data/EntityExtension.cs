using LevendMonopoly.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Data
{
    public static class EntityExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
