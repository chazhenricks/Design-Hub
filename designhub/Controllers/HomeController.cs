using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using designhub.Models;
using designhub.Data;
using Microsoft.EntityFrameworkCore;

namespace designhub.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
          
            _userManager = userManager;
            _context = context;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);



        public async Task<IActionResult> Index(object Test)     
        {
            var user = GetCurrentUserAsync();
            if (User.Identity.IsAuthenticated)
            {

                var projects = await _context.Project.ToListAsync();

                if (projects.Count < 1)
                {
                    return View("LoggedInView");
                }

                return RedirectToAction("Index", "Projects");
                
            }
            return View("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
