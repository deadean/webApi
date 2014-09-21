using Blank.Data.Implementations.Entities;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitySqlite
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
