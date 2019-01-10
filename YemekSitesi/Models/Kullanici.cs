namespace YemekSitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Blog = new HashSet<Blog>();
            Yemek = new HashSet<Yemek>();
        }

        public int kullaniciID { get; set; }

        [StringLength(50), Required(ErrorMessage = "Ad kýsmý boþ geçilemez."), Display(Name = "Ad")]
        public string ad { get; set; }

        [StringLength(50), Required(ErrorMessage = "Soyad kýsmý boþ geçilemez."), Display(Name = "Soyad")]
        public string soyad { get; set; }

        [StringLength(50), Required(ErrorMessage = "Meslegi kýsmý boþ geçilemez."), Display(Name = "Meslegi")]
        public string meslegi { get; set; }

        [StringLength(50), Required(ErrorMessage = "Ulkesi kýsmý boþ geçilemez."), Display(Name = "Ulkesi")]
        public string ulkesi { get; set; }

        [Column(TypeName = "smalldatetime"),Display(Name ="Dogum Tarihi")]
        public DateTime? dogumTarihi { get; set; }

        [StringLength(50), EmailAddress(ErrorMessage = "Uygun bir {0} giriniz."), Display(Name = "E-Posta")]
        public string eposta { get; set; }

        public bool? adminMi { get; set; }
     
        public string sifre { get; set; }

        public bool? aktifMi { get; set; }

        [StringLength(100)]
        public string resim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blog> Blog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yemek> Yemek { get; set; }
    }
}
