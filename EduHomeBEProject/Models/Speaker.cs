using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
