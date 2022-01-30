using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class EComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime WriteTime { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
