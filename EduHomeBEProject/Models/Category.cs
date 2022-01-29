﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public List<Course>  Courses { get; set; }
    }
}
