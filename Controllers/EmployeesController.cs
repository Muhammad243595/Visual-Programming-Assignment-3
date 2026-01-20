using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechNovaLeave.Data;

namespace TechNovaLeave.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Employees.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(EmployeeModel emp)
        {
            if (_context.Employees.Any(e => e.CNIC == emp.CNIC || e.Email == emp.Email))
            {
                ModelState.AddModelError("", "CNIC or Email already exists");
                return View(emp);
            }

            _context.Employees.Add(emp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ToggleStatus(int id)
        {
            var emp = _context.Employees.Find(id);
            emp.IsActive = !emp.IsActive;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
