namespace YemekSitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YorumCevap")]
    public partial class YorumCevap
    {
        public int YorumCevapID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        [StringLength(50)]
        public string icerik { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }

        public bool? cevapOnaylimi { get; set; }

        public int? YorumID { get; set; }

        public virtual Yorum Yorum { get; set; }
    }
}
