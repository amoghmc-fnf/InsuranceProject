using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminApp.Models;
using AdminApp.Services;

namespace AdminApp.Controllers
{
    [Authorize]
    public class HospitalController : Controller
    {
        private readonly IHospitalService _hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        public async Task<IActionResult> ManageHospitals()
        {
            var hospitals = await _hospitalService.GetAllHospitalsAsync();
            return View(hospitals);
        }

        [HttpPost]
        public async Task<IActionResult> AddHospital(Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                await _hospitalService.AddHospitalAsync(hospital);
                return RedirectToAction("ManageHospitals");
            }
            return View("ManageHospitals", await _hospitalService.GetAllHospitalsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHospital(Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                await _hospitalService.UpdateHospitalAsync(hospital);
                return RedirectToAction("ManageHospitals");
            }
            return View("ManageHospitals", await _hospitalService.GetAllHospitalsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHospital(int hospitalId)
        {
            await _hospitalService.DeleteHospitalAsync(hospitalId);
            return RedirectToAction("ManageHospitals");
        }
    }
}