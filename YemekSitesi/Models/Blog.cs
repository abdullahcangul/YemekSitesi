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
        public int blogID { get; set; }

        [StringLength(50)]
        public string baslik { get; set; }

        [StringLength(4000)]
        public string icerik { get; set; }

        [StringLength(50)]
        public string resim { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }

        public int? kullanıcıID { get; set; }

        public int? yorumID { get; set; }

        public int? KategoriID { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Yorum Yorum { get; set; }
    }
}
