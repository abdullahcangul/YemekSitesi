namespace YemekSitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Blog()
        {
            Yorum = new HashSet<Yorum>();
        }

        public int blogID { get; set; }

        [StringLength(50),Required(ErrorMessage = "Baslik kısmı boş geçilemez."), Display(Name = "Baslik")]
        public string baslik { get; set; }
        [Required(ErrorMessage = "İçerik kısmı boş geçilemez."), Display(Name = "İçerik")]
        public string icerik { get; set; }

        [StringLength(50)]
        public string resim { get; set; }

        [Column(TypeName = "smalldatetime"),Display(Name = "Tarih")]
        public DateTime? tarih { get; set; }
        [Display(Name = "Kullanıcılar")]
        public int? kullanıcıID { get; set; }
        [Display(Name = "Kategoriler")]
        public int? KategoriID { get; set; }

        [StringLength(100), Required(ErrorMessage = "Açıklama kısmı boş geçilemez."), Display(Name = "Açıklama")]
        public string aciklama { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorum { get; set; }
    }
}
