using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class NoticeBoard
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string Answer { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; }
        //[Required]
        //[StringLength(maximumLength:200)]
        //public string Link { get; set; }
    }
}
