using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.ViewModels
{
    public class LoginVM
    {
        [Required]
        [StringLength(maximumLength:50)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Remember { get; set; }

    }
}
