using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminApp.Models;
using AdminApp.Services;

namespace AdminApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminService _adminService;

        public HomeController(ILogger<HomeController> logger, IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            // Retrieve the AdminId claim from the user
            var adminIdClaim = User.FindFirst("AdminId")?.Value;

            // Ensure the claim is valid and parse it to an integer
            if (int.TryParse(adminIdClaim, out var adminId))
            {
                try
                {
                    // Fetch admin details by ID
                    var admin = await _adminService.GetAdminByIdAsync(adminId);
                    if (admin != null)
                    {
                        // If admin is found, return the Profile view with the admin model
                        return View(admin);
                    }
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occur during the process
                    _logger.LogError(ex, "Error fetching admin profile.");
                }
            }

            // Redirect to login if admin is not found or there's an error
            return RedirectToAction("Login", "Account");
        }
        public IActionResult Hospitals()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Policies()
        {
            return View();
        }

        public IActionResult Claims()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}