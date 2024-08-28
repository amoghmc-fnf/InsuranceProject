using AdminDbService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApi.Controllers
{
    public interface IPaymentController
    {
        Task<IActionResult> Add(PaymentDto paymentDto);
        Task<IActionResult> Delete(int id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(PaymentDto paymentDto);
    }

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase, IPaymentController
    {
        private readonly IPaymentService service;
        public PaymentController(IPaymentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<PaymentDto> paymentDtos = await service.GetAll();
            return Ok(paymentDtos);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.Delete(id);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(PaymentDto paymentDto)
        {
            PaymentDto addedPaymentDto = await service.Add(paymentDto);
            return Ok(addedPaymentDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentDto paymentDto)
        {
            try
            {
                await service.Update(paymentDto);
                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var found = await service.GetById(id);
                return Ok(found);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}

