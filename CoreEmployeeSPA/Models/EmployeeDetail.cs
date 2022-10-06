using System;
using System.Collections.Generic;

#nullable disable

namespace CoreEmployeeSPA.Models
{
    public partial class EmployeeDetail
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? DepartmentId { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string PicPath { get; set; }
    }
}
