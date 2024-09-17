using DotNetCoreMVC_MakerChecker.ContextClass;
using DotNetCoreMVC_MakerChecker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetCoreMVC_MakerChecker.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyDbContext _context;

        public LoginController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.TBL_CKR_Employees.ToList();
            return View(employees);
        }



        // GET: Login/Login Create View 
        public IActionResult Login()
        {
            return View();
        }

        // POST: User logged in : Login/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login model)
        {
            var user = _context.Logins.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetInt32("UserId", user.UserId);

                if (user.Role == "Admin")
                    return RedirectToAction("AdminDashboard");
                else if (user.Role == "User")
                    return RedirectToAction("UserDashboard");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }

        private IEnumerable<SelectListItem> GetRoles()
        {
            return _context.Logins
                .Select(l => l.Role)
                .Distinct()
                .Select(role => new SelectListItem
                {
                    Value = role,
                    Text = role
                })
                .ToList();
        }
        // GET: User Register : Login/Register
        public IActionResult Register()
        {
            ViewBag.Roles = GetRoles();
            return View();
        }

        // POST: User Register create :  Login/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Logins.Add(login);
                _context.SaveChanges();
                TempData["Message"] = "Registration successful.";
                return RedirectToAction("Login");
            }

            ViewBag.Roles = GetRoles(); // Repopulate roles in case of validation failure
            return View(login);
        }

    }
}