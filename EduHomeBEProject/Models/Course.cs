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


        [Required]
        [DataType(DataType.Date)]
        public DateTime Starts { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string Duration { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string ClassDuration { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string SkillLevel { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string Language { get; set; }
        [Required]
        public int StudentCount { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<CourseTag> CourseTags { get; set; }
        [NotMapped]
        public List<int> TagIds { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
