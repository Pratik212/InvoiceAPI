using System.Collections.Generic;
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
    public class ProductController : Controller
    {
        private readonly InvoiceContext _context;
        private readonly IProductRepository _productRepository;

        public ProductController(InvoiceContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        #region POSTAPI

        [HttpPost]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> Add([FromBody]ProductDto productDto)
        {
            var productData = new Product()
            {
                Description = productDto.Description,
                Qty = productDto.Qty,
                UnitPrice = productDto.UnitPrice,
            };

            productData.Total = productDto.Qty * productDto.UnitPrice;

            productData.SubTotal = productData.Total;

            var result = await _productRepository.AddProduct(productData);

            return Ok(new
            {
                result.Id,
                result.Description,
                result.Qty,
                result.UnitPrice,
                result.Total,
                result.SubTotal
            });
        }

        #endregion

        #region GETAPI
        
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _productRepository.GetAll();
            
            if (product.Count == 0) throw new ApiExceptions($"{Message.P001}");


            return Ok(new
            {
                product
            });
        }
        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> GetProductById(long id)
        {
            var product = await _productRepository.GetProductById(id);
            
            if (product == null) throw new ApiExceptions($"{Message.P002}");
            
            return Ok(new
            {
                product.Id,
                product.Description,
                product.Qty,
                product.UnitPrice,
                product.Total,
                product.SubTotal
            });
        }

        #endregion
    }
}