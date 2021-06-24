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
    public class InvoiceController : Controller
    {
        private readonly InvoiceContext _context;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(InvoiceContext context, IInvoiceRepository invoiceRepository)
        {
            _context = context;
            _invoiceRepository = invoiceRepository;
        }

        #region POSTAPI

        [HttpPost]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> Add([FromBody]InvoiceDto invoiceDto)
        {
            var invoiceData = new Invoice()
            {
                InvoiceNo = invoiceDto.InvoiceNo,
                InvoiceDate = invoiceDto.InvoiceDate,
            };

            var result = await _invoiceRepository.Add(invoiceData);

            return Ok(new
            {
                result.Id,
                result.InvoiceNo,
                result.InvoiceDate,
            });
        }

        #endregion

        #region GETAPI

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetAllInvoice()
        {
            var invoiceData = await _invoiceRepository.GetAll();
            
            if (invoiceData.Count == 0) throw new ApiExceptions($"{Message.I001}");

            return Ok(new
            {
                invoiceData
            });
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetInvoiceById(long id)
        {
            var invoiceData = await _invoiceRepository.GetInvoiceById(id);
            
            if (invoiceData == null) throw new ApiExceptions($"{Message.I002}");
            
            return Ok(new
            {
                invoiceData.Id,
                invoiceData.InvoiceNo,
                invoiceData.InvoiceDate,
            });
        }
        #endregion
    }
}