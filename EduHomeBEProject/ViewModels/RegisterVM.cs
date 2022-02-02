using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [StringLength(maximumLength:60)]
        public string Username { get; set; }
        [StringLength(maximumLength: 60)]
        [Required]
        public string Fullname { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ComfirmPassword { get; set; }
        [StringLength(maximumLength: 60)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool Terms { get; set; }
    }
}
