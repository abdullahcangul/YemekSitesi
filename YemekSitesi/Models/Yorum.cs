namespace YemekSitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yorum")]
    public partial class Yorum
    {
        public int yorumID { get; set; }

        public int? yemekID { get; set; }

        public int? blogID { get; set; }

        [StringLength(50), Required(ErrorMessage = "Adi kýsmý boþ geçilemez."), Display(Name = "Adi")]
        public string ad { get; set; }

        [StringLength(50), Required(ErrorMessage = "Soyad kýsmý boþ geçilemez."), Display(Name = "Soyad")]
        public string soyad { get; set; }

        [StringLength(500), Required(ErrorMessage = "Ýçerik kýsmý boþ geçilemez."), Display(Name = "Ýçerik")]
        public string icerik { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }

        public bool? onaylimi { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual Yemek Yemek { get; set; }
    }
}
