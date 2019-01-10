namespace YemekSitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tarif")]
    public partial class Tarif
    {
        public int tarifID { get; set; }

        public int? yemekID { get; set; }

        public int? siraNo { get; set; }

        [StringLength(500), Required(ErrorMessage = "Aciklama kýsmý boþ geçilemez."), Display(Name = "Aciklama")]
        public string aciklama { get; set; }

        public virtual Yemek Yemek { get; set; }
    }
}
