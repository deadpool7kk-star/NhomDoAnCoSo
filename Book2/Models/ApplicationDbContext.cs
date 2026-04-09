using Book2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<Sach> Saches { get; set; }

        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GioHang>()
                .HasIndex(x => x.UserId)
                .IsUnique();

            builder.Entity<ChiTietGioHang>()
                .HasOne(x => x.GioHang)
                .WithMany(x => x.ChiTietGioHangs)
                .HasForeignKey(x => x.GioHangId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ChiTietGioHang>()
                .HasOne(x => x.Sach)
                .WithMany()
                .HasForeignKey(x => x.SachId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ChiTietDonHang>()
                .HasOne(x => x.DonHang)
                .WithMany(x => x.ChiTietDonHangs)
                .HasForeignKey(x => x.DonHangId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ChiTietDonHang>()
                .HasOne(x => x.Sach)
                .WithMany()
                .HasForeignKey(x => x.SachId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}