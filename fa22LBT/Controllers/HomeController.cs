using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using fa22LBT.DAL;
using fa22LBT.Models;
using fa22LBT.Utilities;

namespace fa22LBT.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
                if (userLoggedIn.IsActive == false)
                {
                    return View("Locked");
                }
            }
            return View();
        }
    }
}