using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmployeeSPA.ViewModel
{
    public class VmEmployeeDetail
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? DepartmentId { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public DateTime? JoiningDate { get; set; }
        public IFormFile PicPath { get; set; }
    }
}
