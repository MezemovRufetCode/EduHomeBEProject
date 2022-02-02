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
        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }
        [StringLength(maximumLength:700)]
        [Required(ErrorMessage = "Please enter Description")]
        public string Description { get; set; }
        [StringLength(maximumLength: 700)]
        [Required(ErrorMessage = "About can not be empty")]
        public string About { get; set; }
        [StringLength(maximumLength: 700)]
        [Required(ErrorMessage = "Students need to know how to apply")]
        public string Apply { get; set; }
        [StringLength(maximumLength: 700)]
        [Required(ErrorMessage = "Certification can not be empty")]
        public string Certification { get; set; }
        [Required(ErrorMessage = "Start Time can not be empty")]
        [DataType(DataType.Date)]
        public DateTime Starts { get; set; }
        [StringLength(maximumLength: 50)]
        [Required(ErrorMessage = "Course duration can not be empty")]
        public string Duration { get; set; }
        [StringLength(maximumLength: 50)]
        [Required(ErrorMessage = "Class duration can not be empty")]
        public string ClassDuration { get; set; }
        [StringLength(maximumLength: 50)]
        [Required(ErrorMessage = "Skill level can not be empty")]
        public string SkillLevel { get; set; }
        [StringLength(maximumLength: 50)]
        [Required(ErrorMessage = "Language can not be empty")]
        public string Language { get; set; }
        [Required(ErrorMessage ="Students count must be included")]
        public int StudentCount { get; set; }
        [Required(ErrorMessage ="Course fee must be included")]
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
