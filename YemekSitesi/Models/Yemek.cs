namespace YemekSitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yemek")]
    public partial class Yemek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yemek()
        {
            BesinDegerleri = new HashSet<BesinDegerleri>();
            Mazeme = new HashSet<Mazeme>();
            Tarif = new HashSet<Tarif>();
            Yorum = new HashSet<Yorum>();
        }

        public int yemekID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        [StringLength(300)]
        public string aciklama { get; set; }

        public int? kategoriID { get; set; }

        [StringLength(50)]
        public string kacKisilik { get; set; }

        public int? pisirmeSuresi { get; set; }

        public int? kullaniciID { get; set; }

        [StringLength(100)]
        public string resim { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }

        public bool? aktifMi { get; set; }

        public int? zorlukDerecesiID { get; set; }

        public int? ulkeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BesinDegerleri> BesinDegerleri { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mazeme> Mazeme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tarif> Tarif { get; set; }

        public virtual Ulkeler Ulkeler { get; set; }

        public virtual ZorlukDerecesi ZorlukDerecesi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorum { get; set; }
    }
}
