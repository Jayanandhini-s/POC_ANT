using Microsoft.EntityFrameworkCore;

namespace POC_ANTO.Models
{
    
        public class POCContext : DbContext
        {
            public POCContext() { }
            public POCContext(DbContextOptions<POCContext> options) : base(options)
            { }
            public DbSet<Invoice>? Invoice { get; set; }
            public DbSet<Item>? Items { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                    optionsBuilder.UseSqlServer("Server=.;Database=Invoice_POC_1;Trusted_Connection=True;TrustServerCertificate=True;");
                }
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Invoice>().HasMany(od => od.Item).WithOne(om => om.Invoice).HasForeignKey(k => k.InvoiceID).OnDelete(DeleteBehavior.Cascade);



            }
            //public DbSet<POC_ANTO.ViewModel.LineItems>? LineItems { get; set; }

        }
    }
