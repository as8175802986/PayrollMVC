using BusinessLayer.Interface;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLayer.UserModel;

namespace EmployeePayrollMVC.Controllers
{
    public class EmployeeController : Controller
    {
        IUserBL UserBL;

        public EmployeeController(IUserBL UserBL)
        {
            this.UserBL = UserBL;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var empList = this.UserBL.EmployeeList();
            return View(empList);

        }

        //[HttpGet("GetAll")]
        //public IActionResult EmployeeList()
        //{
        //    var empList = this.UserBL.EmployeeList();
        //    return View(empList);
        //}
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind] UserModel employee)
        {
            try
            {
                // string insertEmployee = this.UserBL.AddEmployee(model);

                if (ModelState.IsValid)
                {
                    UserBL.AddEmployee(employee);
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserModel employee = UserBL.getEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(int EmployeeID, UserModel employee)
        {
            if (EmployeeID != employee.EmployeeID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                UserBL.UpdateEmployeeDetails(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserModel employee = UserBL.getEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserModel employee = UserBL.getEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            var employee = UserBL.getEmployeeById(id);
            UserBL.deleteEmployee(employee);
            return RedirectToAction("Index");
        }


    }
}
