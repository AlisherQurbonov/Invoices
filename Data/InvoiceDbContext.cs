using Microsoft.EntityFrameworkCore;
using invoice.Entities;

namespace invoice.Data;

public class InvoiceDbContext : DbContext
{
      public DbSet<Category> Categories { get; set; }

      public DbSet<Customer> Customeries { get; set; }

      public DbSet<Detail> Details { get; set; }

      public DbSet<Invoice> Invoices { get; set; }

     public DbSet<Order> Orders { get; set; }

     public DbSet<Payment> Payments { get; set; }

     public DbSet<Product> Products { get; set; }


 

    public InvoiceDbContext(DbContextOptions options)
    : base(options) { }

   
     protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        

        builder.Entity<Customer>(c=>
        {
              c.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.Cust_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

         builder.Entity<Order>(o=>
        {
              o.HasMany(o => o.Invoices)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.Ord_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });


         builder.Entity<Invoice>(i=>
        {
              i.HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.Inv_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });



        builder.Entity<Detail>(i =>
        {
            
            i.HasOne(inv => inv.Order)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            i.HasOne(inv => inv.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

        });


          builder.Entity<Category>(i=>
        {
              i.HasMany(i => i.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.Category_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

    }


}