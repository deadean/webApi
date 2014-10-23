using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Blank.Data.Implementations.Entities;

namespace Blank.Data.SQLite
{
    class ModelContext : DbContext
    {
        public ModelContext()
        {
            Database.SetInitializer<ModelContext>(null);
        } 
        public DbSet<User> USER { get; set; }
        public DbSet<Status> STATUS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Chinook Database does not pluralize table names
            //modelBuilder.Conventions
            //    .Remove<PluralizingTableNameConvention>();

             //one-to-many
            //modelBuilder.Entity<Category>().HasMany<Product>(s => s.Products)
            //.WithRequired(s => s.Category).HasForeignKey(s => s.idCategory);
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Status>();
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
