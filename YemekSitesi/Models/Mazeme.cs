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

        [StringLength(100)]
        public string mazemeAdi { get; set; }

        public double? miktar { get; set; }

        [StringLength(50)]
        public string birim { get; set; }

        public virtual Yemek Yemek { get; set; }
    }
}
