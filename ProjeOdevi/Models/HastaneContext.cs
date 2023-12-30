using Microsoft.EntityFrameworkCore;

namespace ProjeOdevi.Models
{
    public class HastaneContext : DbContext
    {

        public DbSet<Kullanici> Kuallanicilar { get; set; }
        public DbSet<Hasta> Hastalar { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<Birim> Birimler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hasta>().ToTable("Hastalar");
            modelBuilder.Entity<Doktor>().ToTable("Doktorlar");
            modelBuilder.Entity<Admin>().ToTable("Adminler");

            modelBuilder.Entity<Randevu>()
            .HasOne(r => r.Hasta)
            .WithMany(h => h.AlinanRandevular)
            .HasForeignKey(r => r.HastaId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Randevu>()
            .HasOne(r => r.Hasta)
            .WithMany(h => h.AlinanRandevular)
            .HasForeignKey(r => r.HastaId)
            .OnDelete(DeleteBehavior.Restrict);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-4APU5CQ\SQLEXPRESS; 
            Database=HastaneProje;Trusted_Connection=True;");
        }
    }
}
