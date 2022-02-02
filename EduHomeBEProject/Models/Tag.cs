using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Tag name can not be empty")]
        [StringLength(maximumLength:20, ErrorMessage = "Max length can be 20")]
        public string Name { get; set; }
        public List<CourseTag> CourseTags { get; set; }
    }
}
