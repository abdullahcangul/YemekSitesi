namespace YemekSitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Iletisim")]
    public partial class Iletisim
    {
        public int ID { get; set; }

        [StringLength(50), Required(ErrorMessage = "Ýsim kýsmý boþ geçilemez."), Display(Name = "Ýsim")]
        public string isim { get; set; }

        [StringLength(50), EmailAddress(ErrorMessage = "Uygun bir {0} giriniz."), Display(Name = "E-Posta")]
        public string eposta { get; set; }

        [StringLength(50), Required(ErrorMessage = "Telefon kýsmý boþ geçilemez."), Display(Name = "Telefon")]
        public string telefon { get; set; }

        [StringLength(50), Required(ErrorMessage = "Ýçerik kýsmý boþ geçilemez."), Display(Name = "Ýcerik")]
        public string icerik { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }
    }
}
