namespace YemekSitesi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class YemekContext : DbContext
    {
        public YemekContext()
            : base("name=YemekContext")
        {
        }

        public virtual DbSet<BesinDegerleri> BesinDegerleri { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Mazeme> Mazeme { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tarif> Tarif { get; set; }
        public virtual DbSet<Ulkeler> Ulkeler { get; set; }
        public virtual DbSet<Yemek> Yemek { get; set; }
        public virtual DbSet<Yorum> Yorum { get; set; }
        public virtual DbSet<YorumCevap> YorumCevap { get; set; }
        public virtual DbSet<ZorlukDerecesi> ZorlukDerecesi { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Blog)
                .WithOptional(e => e.Kullanici)
                .HasForeignKey(e => e.kullanıcıID);
        }
    }
}
