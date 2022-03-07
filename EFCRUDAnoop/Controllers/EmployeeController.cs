using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCRUDAnoop.Models;

namespace EFCRUDAnoop.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                dbContext.Employees.Add(emp);
                dbContext.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return View(emp);
            }
        }

        public IActionResult Update(int id)
        {
            var DbCheckEmp = dbContext.Employees.SingleOrDefault(e => e.Id == id);
            return View(DbCheckEmp);
        }
        [HttpPost]
        public IActionResult(Employee emp)
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
