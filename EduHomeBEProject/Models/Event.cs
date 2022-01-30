using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime EventDay { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public List<EComment> EComments { get; set; }
    }
}
