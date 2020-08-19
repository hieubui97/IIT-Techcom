namespace IITtechcom.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Information
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NameCompany { get; set; }

        [StringLength(100)]
        public string Logo { get; set; }

        [StringLength(13)]
        public string Tel { get; set; }

        [StringLength(13)]
        public string Hotline { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Column(TypeName = "ntext")]
        public string Terms_Conditions { get; set; }

        [StringLength(1000)]
        public string Decription { get; set; }

        [StringLength(500)]
        public string MetaTitle { get; set; }

        [StringLength(1000)]
        public string MetaDecription { get; set; }
    }
}
