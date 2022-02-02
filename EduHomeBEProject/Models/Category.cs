using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Category name can not be empty")]
        public string Name { get; set; }
        public List<Course>  Courses { get; set; }
    }
}
