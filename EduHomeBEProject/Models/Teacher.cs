using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Position { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string About { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [StringLength(maximumLength: 50)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        [StringLength(maximumLength: 50)]
        public string Phone { get; set; }
        public List<TSocials> Socials { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
