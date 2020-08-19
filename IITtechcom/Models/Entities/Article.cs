namespace IITtechcom.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        public int ID { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        public int MenuID { get; set; }

        public DateTime? CreateDate { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public bool? Status { get; set; }

        public bool? IsActive { get; set; }

        public bool? ShowHome { get; set; }

        [StringLength(1000)]
        public string Decription { get; set; }

        [StringLength(500)]
        public string MetaTitle { get; set; }

        [StringLength(1000)]
        public string MetaDecription { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
