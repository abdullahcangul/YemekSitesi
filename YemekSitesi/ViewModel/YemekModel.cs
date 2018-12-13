using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YemekSitesi.ViewModel
{
    public class YemekModel
    {
        public int yemekID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        public int? kategoriID { get; set; }

        [StringLength(50)]
        public string kacKisilik { get; set; }

        public int? pisirmeSuresi { get; set; }

        public int? kullaniciID { get; set; }

        [StringLength(100)]
        public string resim { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }

        public bool? aktifMi { get; set; }

        public int? zorlukDerecesiID { get; set; }

        public int? ulkeID { get; set; }


        public int mazemeID { get; set; }

        public int? MazemeYemekID { get; set; }

        [StringLength(100)]
        public string mazemeAdi { get; set; }

        public double? miktar { get; set; }

        [StringLength(50)]
        public string birim { get; set; }



        public int besinDegerID { get; set; }

        public int? BasinYemekID { get; set; }

        [StringLength(50)]
        public string besinAdi { get; set; }

        [StringLength(50)]
        public string deger { get; set; }



        public int tarifID { get; set; }

        public int? TarifYemekID { get; set; }

        public int? siraNo { get; set; }

        [StringLength(500)]
        public string aciklama { get; set; }

    }
}