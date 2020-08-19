namespace IITtechcom.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            Articles = new HashSet<Article>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        public int? ParentID { get; set; }

        public int? Level { get; set; }

        public bool? Status { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(500)]
        public string Decription { get; set; }

        [StringLength(200)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        public string MetaDecription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }
    }
}
