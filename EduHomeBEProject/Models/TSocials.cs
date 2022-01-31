using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class TSocials
    {
        public int Id { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string Pinterest { get; set; }
        public string Vimeo { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
    }
}
