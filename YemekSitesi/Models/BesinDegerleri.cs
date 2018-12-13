namespace YemekSitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BesinDegerleri")]
    public partial class BesinDegerleri
    {
        [Key]
        public int besinDegerID { get; set; }

        public int? yemekID { get; set; }

        [StringLength(50)]
        public string besinAdi { get; set; }

        [StringLength(50)]
        public string deger { get; set; }

        public virtual Yemek Yemek { get; set; }
    }
}
