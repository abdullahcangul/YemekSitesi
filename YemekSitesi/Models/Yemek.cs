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

        [StringLength(50), Required(ErrorMessage = "Ad  kýsmý boþ geçilemez."), Display(Name = "Ad")]
        public string ad { get; set; }

        [StringLength(300), Required(ErrorMessage = "Aciklama kýsmý boþ geçilemez."), Display(Name = "Aciklama")]
        public string aciklama { get; set; }
        [Display(Name = "Kategoriler")]
        public int? kategoriID { get; set; }

        [StringLength(50), Required(ErrorMessage = "Kaç kiþilik kýsmý boþ geçilemez."), Display(Name = "Kaç kiþilik")]
        public string kacKisilik { get; set; }
        [Required(ErrorMessage = "Pisirme Suresi kýsmý boþ geçilemez."), Display(Name = "Pisirme Suresi")]
        public int? pisirmeSuresi { get; set; }
        [Display(Name = "Kullanýcýlar")]
        public int? kullaniciID { get; set; }

        [StringLength(100)]
        public string resim { get; set; }

        [Column(TypeName = "smalldatetime"),Display(Name = "Tarih")]
        public DateTime? tarih { get; set; }

        public bool? aktifMi { get; set; }
        [Display(Name = "Zorluk Derecesi")]
        public int? zorlukDerecesiID { get; set; }
        [Display(Name = "Ulkeler")]
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
