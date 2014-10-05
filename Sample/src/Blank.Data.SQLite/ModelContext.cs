using System.Data.Entity;
using Blank.Data.Implementations.Entities;

namespace Blank.Data.SQLite
{
    class ModelContext : DbContext
    {
        public DbSet<User> USER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Chinook Database does not pluralize table names
            //modelBuilder.Conventions
            //    .Remove<PluralizingTableNameConvention>();

             //one-to-many
            //modelBuilder.Entity<Category>().HasMany<Product>(s => s.Products)
            //.WithRequired(s => s.Category).HasForeignKey(s => s.idCategory);
            modelBuilder.Entity<User>();
        }
    }
}
