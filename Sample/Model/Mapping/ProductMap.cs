using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ConsoleApplication2.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.number)
                .IsRequired()
                .HasMaxLength(2147483647);

            this.Property(t => t.name)
                .HasMaxLength(2147483647);

            this.Property(t => t.image)
                .IsRequired()
                .HasMaxLength(2147483647);

            this.Property(t => t.description)
                .IsRequired()
                .HasMaxLength(2147483647);

            // Table & Column Mappings
            this.ToTable("Products");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.idCategory).HasColumnName("idCategory");
            this.Property(t => t.number).HasColumnName("number");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.image).HasColumnName("image");
            this.Property(t => t.cost).HasColumnName("cost");
            this.Property(t => t.description).HasColumnName("description");

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.idCategory);

        }
    }
}
