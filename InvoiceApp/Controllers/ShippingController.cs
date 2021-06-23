using System.Linq;
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
    public class ShippingController : Controller
    {
        private readonly InvoiceContext _context;
        private readonly IShippingRepository _shippingRepository;

        public ShippingController(InvoiceContext context, IShippingRepository shippingRepository)
        {
            _context = context;
            _shippingRepository = shippingRepository;
        }

        #region POSTAPI

        [HttpPost]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult>Add([FromBody]ShippingDto shippingDto)
        {
            var shipAdd = new Shipping()
            {
                DepartmentName = shippingDto.DepartmentName,
                ClientCompany = shippingDto.ClientCompany,
                Address = shippingDto.Address,
                PhoneNumber = shippingDto.PhoneNumber
            };
            
            var result = await _shippingRepository.Add(shipAdd);

            return Ok(new
            {
                result.Id,
                result.DepartmentName,
                result.ClientCompany,
                result.Address,
                result.PhoneNumber
            });

        }

        #endregion

        #region GETAPI

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetAll()
        {
            var shipAdd = await _shippingRepository.GetAll();
            
            if (shipAdd.Count() == 0) throw new ApiExceptions($"{Message.S001}");

            return Ok(new
            {
                shipAdd
            });
        }
        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetById(long id)
        {
            var shipAdd = await _shippingRepository.GetById(id);
            
            if (shipAdd == null) throw new ApiExceptions($"{Message.S002}");

            return Ok(new
            {
                shipAdd.Id,
                shipAdd.DepartmentName,
                shipAdd.ClientCompany,
                shipAdd.Address,
                shipAdd.PhoneNumber
            });
        }

        #endregion
    }
}