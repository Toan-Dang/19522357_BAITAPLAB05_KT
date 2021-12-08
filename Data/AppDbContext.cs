using Microsoft.EntityFrameworkCore;
using _BAITAPLAB05_KT.Models;

namespace _BAITAPLAB05_KT.Data {

    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder
       modelBuilder) {
            modelBuilder.Entity<CongNhan>().ToTable("congnhan");

            modelBuilder.Entity<TrieuChung>().ToTable("trieuchung");

            modelBuilder.Entity<DiemCachLy>().ToTable("diemcachly");
            modelBuilder.Entity<CN_TC>().ToTable("cn_tc").HasKey(c => new { c.MaCongNhan, c.MaTrieuChung });
        }

        public DbSet<_BAITAPLAB05_KT.Models.DiemCachLy> DiemCachLy { get; set; }

        public DbSet<_BAITAPLAB05_KT.Models.TrieuChung> TrieuChung { get; set; }

        public DbSet<_BAITAPLAB05_KT.Models.CN_TC> CN_TC { get; set; }

        public DbSet<_BAITAPLAB05_KT.Models.CongNhan> CongNhan { get; set; }
    }
}