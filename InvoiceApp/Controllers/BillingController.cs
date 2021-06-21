using System.Threading.Tasks;
using InvoiceApp.Dtos;
using InvoiceApp.Exceptions;
using InvoiceApp.Helpers;
using InvoiceApp.Interfaces;
using InvoiceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : Controller
    {
        private readonly IBillingRepository _billingRepository;
        private readonly InvoiceContext _context;

        public BillingController(InvoiceContext context, IBillingRepository billingRepository)
        {
            _context = context;
            _billingRepository = billingRepository;
        }
        
        #region POSTAPI

        [HttpPost]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add([FromForm] BillingDto billingDto)
        {
            var billAdd = new Billing()
            {
                ContactName = billingDto.ContactName,
                CompanyName = billingDto.CompanyName,
                Email = billingDto.Email,
                Address = billingDto.Address,
                PhoneNumber = billingDto.PhoneNumber
            };
            
            billAdd = await _billingRepository.AddBill(billAdd);

            await _billingRepository.SaveChanges();
            return Ok(new
            {
                billAdd.Id,
                billAdd.ContactName,
                billAdd.CompanyName,
                billAdd.Email,
                billAdd.Address,
                billAdd.PhoneNumber
            });
        }

        #endregion

        #region GET API

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetAll()
        {
            var billing = await _billingRepository.GetAll();
            
            if (billing == null) throw new ApiExceptions($"{Message.B001}");

            return Ok(new
            {
                billing
            });
        }
        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetBillingById(long id)
        {
            var billing = await _billingRepository.GetBillingById(id);
            
            if (billing.Id != id) throw new ApiExceptions($"{Message.B002}");

            return Ok(new
            {
                billing.Id,
                billing.ContactName,
                billing.CompanyName,
                billing.Email,
                billing.Address,
                billing.PhoneNumber,
            });
        }

        #endregion
    }
}