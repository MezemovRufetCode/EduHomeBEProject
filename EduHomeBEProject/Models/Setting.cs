using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string TopHeaderAnnounce { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Logo { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string WelcomeEntry { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string CourseSecTitle { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string EventSecTitle { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string BlogSecTitle { get; set; }
        [StringLength(maximumLength: 200)]
        [Required]
        public string SubscribeSecTitle { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string FooterLogo { get; set; }
        [StringLength(maximumLength: 400)]
        [Required]
        public string FooterDesc { get; set; }
        [StringLength(maximumLength: 200)]
        [Required]
        public string FacebookLink { get; set; }
        [StringLength(maximumLength: 200)]
        [Required]
        public string PinterestLink { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string TwitterLink { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string VimeoLink { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Adress { get; set; }
        [StringLength(maximumLength: 100)]
        [Required]
        public string FContact { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string SContact { get; set; }
        [StringLength(maximumLength: 200)]
        [Required]
        public string Email { get; set; }
        [NotMapped]
        public IFormFile  HLImgFile { get; set; }
        [NotMapped]
        public IFormFile FLImgFile { get; set; }

    }
}
