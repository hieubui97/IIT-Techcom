using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IITtechcom.Models.Entities
{
    public class ContactCaptcha
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(13)]
        public string Phone { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        public string Request { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        public string CaptchaCode { get; set; }
    }
}