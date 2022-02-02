using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class TeacherHobby
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int HobbyId { get; set; }
        public Hobby Hobby { get; set; }
    }
}
