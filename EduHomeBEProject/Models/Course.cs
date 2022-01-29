using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:200)]
        public string Image { get; set; }
        //deyesen namei category adi ile eynidir
        [StringLength(maximumLength:50)]
        [Required]
        public string Name { get; set; }
        [StringLength(maximumLength:700)]
        [Required]
        public string Description { get; set; }
        [StringLength(maximumLength: 700)]
        [Required]
        public string About { get; set; }
        [StringLength(maximumLength: 700)]
        [Required]
        public string Apply { get; set; }
        [StringLength(maximumLength: 700)]
        [Required]
        public string Certification { get; set; }
        public string Category { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<CourseTag> CourseTags { get; set; }
        [NotMapped]
        public List<int> TagIds { get; set; }
        public List<Comment> Comments { get; set; }
        public  List<CourseFeature> CourseFeatures { get; set; }
    }
}
