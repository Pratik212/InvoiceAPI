using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceApp.Dtos;
using InvoiceApp.Exceptions;
using InvoiceApp.Helpers;
using InvoiceApp.Interfaces;
using InvoiceApp.Models;
using InvoiceApp.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        [HttpPost]
        [Route("delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteProduct([FromBody] List<long> productIds)
        {
            await _productRepository.Delete(productIds);

            return Ok(new Response<string>(null, "Product successfully deleted."));
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

        #region PUTAPI

        [HttpPut]
        [Route("{productId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateProduct(long productId, [FromBody] ProductDto productDto)
        {
            var productObj =
                await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (productObj == null)
                throw new ApiExceptions($"{Message.P001}");

            productObj.Description = productDto.Description;
            productObj.Qty = productDto.Qty;
            productObj.UnitPrice = productDto.UnitPrice;
            productObj.Total =  productObj.Qty * productObj.UnitPrice;
            
            await _context.SaveChangesAsync();

            return Ok(new Response<dynamic>(new
            {
                productObj.Id,
                productObj.Description,
                productObj.Qty,
                productObj.UnitPrice,
                productObj.Total,
            }, "Product successfully updated."));
        }

        #endregion
    }
}