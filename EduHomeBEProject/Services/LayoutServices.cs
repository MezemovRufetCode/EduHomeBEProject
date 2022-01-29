using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Services
{
    public class LayoutServices
    {
        private readonly AppDbContext _context;
        public LayoutServices(AppDbContext context)
        {
            _context = context;
        }
        public Setting getSettingDatas()
        {
            Setting data = _context.Settings.FirstOrDefault();
            return data;
        }
    }
}
