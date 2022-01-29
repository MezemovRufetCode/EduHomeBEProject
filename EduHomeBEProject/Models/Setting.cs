using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public string TopHeaderAnnounce { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Logo { get; set; }
        public string CourseSecTitle { get; set; }
        //public string CourseSecImg { get; set; }
        [StringLength(maximumLength: 200)]
        public string EventSecTitle { get; set; }
        [StringLength(maximumLength: 200)]
        public string BlogSecTitle { get; set; }
        [StringLength(maximumLength: 200)]
        public string SubscribeSecTitle { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string FooterLogo { get; set; }
        [StringLength(maximumLength:200)]
        public string FacebookLink { get; set; }
        [StringLength(maximumLength: 200)]
        public string PinterestLink { get; set; }
        [StringLength(maximumLength: 200)]
        public string TwitterLink { get; set; }
        [StringLength(maximumLength: 200)]
        public string VimeoLink { get; set; }
        [StringLength(maximumLength: 200)]
        public string FAdress { get; set; }
        [StringLength(maximumLength: 100)]
        public string FContact { get; set; }
        [StringLength(maximumLength: 100)]
        public string SContact { get; set; }
        [StringLength(maximumLength: 200)]
        public string Email { get; set; }

    }
}
