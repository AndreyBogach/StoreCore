namespace StoreCore.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StoreCoreContext : DbContext
    {
        public StoreCoreContext()
            : base("name=StoreCoreContext")
        {
        }

        public virtual DbSet<inf_address> inf_address { get; set; }
        public virtual DbSet<inf_client> inf_client { get; set; }
        public virtual DbSet<inf_legal_entity> inf_legal_entity { get; set; }
        public virtual DbSet<inf_order> inf_order { get; set; }
        public virtual DbSet<inf_physical_entity> inf_physical_entity { get; set; }
        public virtual DbSet<inf_product> inf_product { get; set; }
        public virtual DbSet<inf_type> inf_type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<inf_address>()
                .HasMany(e => e.inf_legal_entity)
                .WithRequired(e => e.inf_address)
                .HasForeignKey(e => e.address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<inf_address>()
                .HasMany(e => e.inf_physical_entity)
                .WithRequired(e => e.inf_address)
                .HasForeignKey(e => e.address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<inf_client>()
                .HasOptional(e => e.inf_legal_entity)
                .WithRequired(e => e.inf_client);

            modelBuilder.Entity<inf_client>()
                .HasMany(e => e.inf_order)
                .WithRequired(e => e.inf_client)
                .HasForeignKey(e => e.client_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<inf_client>()
                .HasOptional(e => e.inf_physical_entity)
                .WithRequired(e => e.inf_client);

            modelBuilder.Entity<inf_order>()
                .Property(e => e.amount)
                .HasPrecision(15, 2);

            modelBuilder.Entity<inf_product>()
                .Property(e => e.price)
                .HasPrecision(15, 2);

            modelBuilder.Entity<inf_product>()
                .HasMany(e => e.inf_order)
                .WithRequired(e => e.inf_product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<inf_type>()
                .HasMany(e => e.inf_client)
                .WithRequired(e => e.inf_type)
                .HasForeignKey(e => e.type_id)
                .WillCascadeOnDelete(false);
        }
    }
}
