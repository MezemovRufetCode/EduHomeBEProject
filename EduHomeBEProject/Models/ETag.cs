using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class ETag
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Max length can be 20")]
        public string Name { get; set; }
        public List<EventTag> EventTags { get; set; }
    }
}
