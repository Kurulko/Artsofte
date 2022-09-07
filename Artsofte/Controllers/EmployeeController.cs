using Artsofte.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using Artsofte.Extensions;

namespace Artsofte.Controllers
{
    public class EmployeeController : Controller
    {
        readonly ArtsofteContext context;
        public EmployeeController(ArtsofteContext context)
            => this.context = context;

        [HttpGet("/")]
        public IActionResult Employees()
        {
            var employees = context.Employees.FromSqlRaw("GetEmployees").ToList();
            foreach (Employee employee in employees)
                employee.IncludeLanguageAndDepartment(context);

            return View(employees);
        }


        [HttpGet("/add")]
        public IActionResult AddEmployee()
            => View();
        [HttpPost("/add")]
        public IActionResult AddEmployee(Employee employee)
        {
            var(_, parameterName, parameterSurname, parameterAge, parameterLanguageId, parameterDepartmentId) = employee;

            int result = context.Database.ExecuteSqlRaw("AddEmployee @Name,@Surname,@Age,@LanguageId,@DepartmentId", parameterName, parameterSurname, parameterAge, parameterLanguageId, parameterDepartmentId);

            return result == 1 ? RedirectToAction(nameof(Employees)) : View(employee);
        }


        [HttpGet("/edit")]
        public IActionResult EditEmployee(int? id)
        {
            SqlParameter parameterId = new("@Id", id);
            Employee? employee = context.Employees.FromSqlRaw("GetEmployeesById @Id", parameterId).AsEnumerable()?.First();

            return employee is not null ? View(employee) : RedirectToAction(nameof(Employees));
        }
        [HttpPost("/edit")]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            var (parameterId, parameterName, parameterSurname, parameterAge, parameterLanguageId, parameterDepartmentId) = employee;

            int result = context.Database.ExecuteSqlRaw("EditEmployee @Id,@Name,@Surname,@Age,@LanguageId,@DepartmentId", parameterId,parameterName, parameterSurname, parameterAge, parameterLanguageId, parameterDepartmentId);

            return result == 1 ? RedirectToAction(nameof(Employees)) : View(employee);
        }


        [HttpGet("/delete")]
        public IActionResult DeleteEmployee(int? id)
        {
            SqlParameter parameterId = new("@Id", id);
            context.Database.ExecuteSqlRaw("DeleteEmployeeById @Id", parameterId);
            return RedirectToAction(nameof(Employees));
        }
    }

    /*
     * OR
    public class EmployeeController : Controller
    {
        readonly ArtsofteContext context;
        public EmployeeController(ArtsofteContext context)
            => this.context = context;

        [HttpGet("/")]
        public IActionResult Employees()
            => View(context.Employees.Include(e => e.Language).Include(e => e.Department).ToList());

        [HttpGet("/add")]
        public IActionResult AddEmployee()
            => View();
        [HttpPost("/add")]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Employees));
            }
            return View(employee);
        }

        [HttpGet("/edit")]
        public IActionResult EditEmployee(int? id)
        {
            if(id is int _id)
            {
                Employee? employee = context.Employees.Include(e => e.Language).Include(e => e.Department).FirstOrDefault(e => e.Id == _id);
                if (employee is not null)
                    return View(employee);
            }
            return RedirectToAction(nameof(Employees));
        }
        [HttpPost("/edit")]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                context.Employees.Update(employee);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Employees));
            }
            return View(employee);
        }

        [HttpGet("/delete")]
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if (id is int _id)
            {
                Employee? employee = context.Employees.Include(e => e.Language).Include(e => e.Department).FirstOrDefault(e => e.Id == _id);
                if (employee is not null)
                {
                    context.Employees.Remove(employee);
                    await context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Employees));
        }
    }
    */
}