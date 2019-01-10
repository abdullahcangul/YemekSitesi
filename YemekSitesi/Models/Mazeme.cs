namespace YemekSitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mazeme")]
    public partial class Mazeme
    {
        public int mazemeID { get; set; }

        public int? yemekID { get; set; }

        [StringLength(100), Required(ErrorMessage = "Mazeme Adi kýsmý boþ geçilemez."), Display(Name = "Mazeme Adi")]
        public string mazemeAdi { get; set; }

        public double? miktar { get; set; }

        [StringLength(50), Required(ErrorMessage = "Birim kýsmý boþ geçilemez."), Display(Name = "Birim")]
        public string birim { get; set; }

        public virtual Yemek Yemek { get; set; }
    }
}
