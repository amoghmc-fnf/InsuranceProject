using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AdminApp.Services;

namespace AdminApp.Controllers
{
    public class PlansController : Controller
    {
        private readonly IInsuranceService _insuranceService;

        public PlansController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        public IActionResult ManagePlans()
        {
            return View();
        }

        public async Task<IActionResult> ViewActivePlans()
        {
            // Fetch active plans from the service
            var activePlans = await _insuranceService.GetActivePlansAsync();
            return View(activePlans);
        }

        public async Task<IActionResult>AddNewPlan(InsuranceTypeDto newPlan)
        {
            // Your logic to add a new plan (form view)
            if (newPlan != null && ModelState.IsValid)
            {
                bool success = await _insuranceService.AddPlanAsync(newPlan);
                if (success)
                {
                    return RedirectToAction("ViewActivePlans");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to add new plan");
                }
            }
            return View(newPlan);
            
        }


        [HttpPut]
        public async Task<IActionResult> UpdatePlan([FromBody] InsuranceTypeDto plan)
        {
            var success = await _insuranceService.UpdatePlanAsync(plan);
            if (success)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlan(int id)
        {
            var success = await _insuranceService.DeletePlanAsync(id);
            if (success)
                return Ok();
            else
                return BadRequest();
        }
    }
}