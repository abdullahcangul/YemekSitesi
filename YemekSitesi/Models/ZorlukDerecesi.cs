namespace YemekSitesi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ZorlukDerecesi")]
    public partial class ZorlukDerecesi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ZorlukDerecesi()
        {
            Yemek = new HashSet<Yemek>();
        }

        public int zorlukDerecesiID { get; set; }

        [StringLength(50), Required(ErrorMessage = "Zorluk Tanımı kısmı boş geçilemez."), Display(Name = "Zorluk Tanımı")]
        public string zorlukTanımı { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yemek> Yemek { get; set; }
    }
}
