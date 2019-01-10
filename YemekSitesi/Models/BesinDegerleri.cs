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

        [StringLength(50), Required(ErrorMessage = "BesinAdi k�sm� bo� ge�ilemez."), Display(Name = "BesinAdi")]
        public string besinAdi { get; set; }

        [StringLength(50), Required(ErrorMessage = "Deger k�sm� bo� ge�ilemez."), Display(Name = "Deger")]
        public string deger { get; set; }

        public virtual Yemek Yemek { get; set; }
    }
}
