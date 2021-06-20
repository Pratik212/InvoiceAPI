using System.Threading.Tasks;
using InvoiceApp.Dtos;
using InvoiceApp.Interfaces;
using InvoiceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly InvoiceContext _context;

        public CompanyController(InvoiceContext context, ICompanyRepository companyRepository)
        {
            _context = context;
            _companyRepository = companyRepository;
        }
        
        [HttpPost]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add([FromForm] CompanyDto companyDto)
        {
            var company = new Company()
            {
                Name = companyDto.Name,
                Address = companyDto.Address,
                Email = companyDto.Email,
                PhoneNumber = companyDto.PhoneNumber
            };

            company = await _companyRepository.AddCompany(company);
            
            await _companyRepository.SaveChanges();
            return Ok(new
            {
                company.Id,
                company.Name,
                company.Email,
                company.Address,
                company.PhoneNumber
            });
        }
    }
}