using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCRUDAnoop.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace EFCRUDAnoop.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext dbContext;
        private IHostingEnvironment Environment;

        public EmployeeController(ApplicationDbContext dbContext,IHostingEnvironment environment)
        {
            this.dbContext = dbContext;
            Environment = environment;
        }

        public IActionResult Index()
        {
            List<Employee> employee = dbContext.Employees.ToList();
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult Create(Employee emp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        dbContext.Employees.Add(emp);
        //        dbContext.SaveChanges();
        //        return RedirectToAction("index");
        //    }
        //    else
        //    {
        //        return View(emp);
        //    }
        //}

        public IActionResult Create(Employee M )
        {
           if (ModelState.IsValid)
            {
                string name = Request.Form["Name"];
                string email = Request.Form["Email"];
                string mobile = Request.Form["Mobile"];
                string addreess = Request.Form["Addreess"];
                var files = Request.Form.Files;
                string dbpath = string.Empty;
                
                if (files.Count > 0)
                {
                    var file = files[0];
                    string path = Environment.WebRootPath;
                    string fullpath =Path.Combine(path, "Images", file.FileName);
                    FileStream stream = new FileStream(fullpath, FileMode.Create);
                    file.CopyTo(stream);
                    dbpath = file.FileName;
                }
                var emp = new Employee()
                {
                    Name = name,
                    Email = email,
                    Mobile = mobile,
                    Addreess = addreess ,
                    Images = dbpath,
                };
                dbContext.Employees.Add(emp);
                dbContext.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Update(int id)
        {
            var DbCheckEmp = dbContext.Employees.SingleOrDefault(e => e.Id == id);
            return View(DbCheckEmp);
        }
        [HttpPost]
        public IActionResult Update(Employee emp)
        {
            dbContext.Employees.Update(emp);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete A Record 
        public IActionResult Delete(int id)
        {
            var emplCheck = dbContext.Employees.SingleOrDefault(e => e.Id == id);
            if (emplCheck != null)
            {
                dbContext.Employees.Remove(emplCheck);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}
