using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime WriteTime { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
