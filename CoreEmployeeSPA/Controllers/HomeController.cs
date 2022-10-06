using CoreEmployeeSPA.Models;
using CoreEmployeeSPA.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmployeeSPA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Department()
        {
            var _ctx = new EmployeeDBContext();
            return Json(_ctx.Departments.OrderBy(x => x.DepartmentName).ToList());
        }

        [HttpGet]
        public IActionResult EmployeeDetailss()
        {
            var _ctx = new EmployeeDBContext();
            var listDetail = (from c in _ctx.EmployeeDetails
                              join d in _ctx.Departments on c.DepartmentId equals d.DepartmentId
                              select new
                              {
                                  c.EmployeeId,
                                  c.EmployeeName,
                                  c.DepartmentId,
                                  c.JoiningDate,
                                  c.Age,
                                  c.Gender,
                                  c.PicPath,
                                  d.DepartmentName
                              }).ToList();
            return Json(listDetail);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var _ctx = new EmployeeDBContext();
            return Json(_ctx.EmployeeDetails.Where(x => x.EmployeeId == id).FirstOrDefault());
        }

        [RequestSizeLimit(2147483648)]
        [HttpPost]
        public IActionResult DetailsAdd([FromForm] VmEmployeeDetail Employee)
        {
            object res = null; var _ctx = new EmployeeDBContext();
            var oDetails = _ctx.EmployeeDetails.Where(x => x.EmployeeId == Employee.EmployeeId).FirstOrDefault();
            if (oDetails == null)
            {
                #region file
                string fileName = "";
                if (Employee.PicPath != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pics");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileInfo fileInfo = new FileInfo(Employee.PicPath.FileName);
                    string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    fileName = newFileName + fileInfo.Extension;
                    string fileNameWithPath = Path.Combine(path, fileName);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Employee.PicPath.CopyTo(stream);
                    }
                }
                #endregion
                oDetails = new EmployeeDetail();
                oDetails.EmployeeName = Employee.EmployeeName;
                oDetails.DepartmentId = Employee.DepartmentId;
                oDetails.JoiningDate = Employee.JoiningDate;
                oDetails.Age = Employee.Age;
                oDetails.Gender = Employee.Gender;
                oDetails.PicPath = fileName;
                _ctx.Add(oDetails);
                _ctx.SaveChanges();
                res = new
                {
                    resstate = true
                };
            }
            return Json(res);
        }

        [HttpPut]
        public IActionResult DetailsEdit([FromForm] VmEmployeeDetail Employee)
        {
            object res = null; var _ctx = new EmployeeDBContext();
            var oDetails = _ctx.EmployeeDetails.Where(x => x.EmployeeId == Employee.EmployeeId).FirstOrDefault();
            if (oDetails != null)
            {
                #region file
                string fileName = "";
                if (Employee.PicPath != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pics");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileInfo fileInfo = new FileInfo(Employee.PicPath.FileName);
                    string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    fileName = newFileName + fileInfo.Extension;
                    string fileNameWithPath = Path.Combine(path, fileName);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        Employee.PicPath.CopyTo(stream);
                    }
                    if (!string.IsNullOrEmpty(oDetails.PicPath))
                    {
                        string fileNameWithPathDEL = Path.Combine(path, oDetails.PicPath);
                        if (System.IO.File.Exists(fileNameWithPathDEL))
                        {
                            System.IO.File.Delete(fileNameWithPathDEL);
                        }
                    }
                }
                #endregion
                oDetails.EmployeeName = Employee.EmployeeName;
                oDetails.DepartmentId = Employee.DepartmentId;
                oDetails.JoiningDate = Employee.JoiningDate;
                oDetails.Age = Employee.Age;
                oDetails.Gender = Employee.Gender;
                oDetails.PicPath = fileName;
                _ctx.SaveChanges();
                res = new
                {
                    resstate = true
                };
            }
            return Json(res);
        }

        [HttpDelete]
        public IActionResult DetailsRemove([FromQuery] int id)
        {
            object res = null; var _ctx = new EmployeeDBContext();
            var oDetails = _ctx.EmployeeDetails.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (oDetails != null)
            {
                _ctx.EmployeeDetails.Remove(oDetails);
                _ctx.SaveChanges();
                #region file
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pics");
                if (!string.IsNullOrEmpty(oDetails.PicPath))
                {
                    string fileNameWithPathDEL = Path.Combine(path, oDetails.PicPath);
                    if (System.IO.File.Exists(fileNameWithPathDEL))
                    {
                        System.IO.File.Delete(fileNameWithPathDEL);
                    }
                }
                #endregion
                res = new
                {
                    resstate = true
                };
            }
            return Json(res);
        }
    }
}
