using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Position { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string About { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [StringLength(maximumLength: 50)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        [StringLength(maximumLength: 50)]
        public string Phone { get; set; }

        [Required]
        [StringLength(maximumLength:100)]
        public string Experience { get; set; }
        [Required]
        [StringLength(maximumLength:100)]
        public string Degree { get; set; }
        //sonra vaxt qalsa duzelt
        [StringLength(maximumLength: 200)]
        public string FacebookAccount { get; set; }
        [StringLength(maximumLength: 200)]
        public string VimeoAccount { get; set; }
        [StringLength(maximumLength: 200)]
        public string Pinterest { get; set; }
        [StringLength(maximumLength: 200)]
        public string TwitterAccount { get; set; }
        public List<TSocials> Socials { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<TeacherFaculty> TeacherFaculties { get; set; }
        [NotMapped]
        public List<int> FacultyIds { get; set; }
        public List<TeacherHobby> TeacherHobbies { get; set; }
        [NotMapped]
        public List<int> HobbyIds { get; set; }



        //public List<string> Features { get; set; }



        public string Feature1 { get; set; }
        public int FeatureVal1 { get; set; }
        public string Feature2 { get; set; }
        public int FeatureVal2 { get; set; }
        public string Feature3 { get; set; }
        public int FeatureVal3 { get; set; }
        public string Feature4 { get; set; }
        public int FeatureVal4 { get; set; }
        public string Feature5 { get; set; }
        public int FeatureVal5 { get; set; }
        public string Feature6 { get; set; }
        public int FeatureVal6 { get; set; }
    }
}
