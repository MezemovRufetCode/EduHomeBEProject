using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeacherFaculty> TeacherFaculties { get; set; }
    }
}
