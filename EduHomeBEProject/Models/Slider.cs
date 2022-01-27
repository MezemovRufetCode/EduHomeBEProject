using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength:300)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 400)]
        public string Desc { get; set; }

        public string Link { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
