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
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly InvoiceContext _context;

        public CompanyController(InvoiceContext context, ICompanyRepository companyRepository)
        {
            _context = context;
            _companyRepository = companyRepository;
        }

        #region POSTAPI

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

        #endregion

        #region GETAPI

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyRepository.GetCompany();
            
            if (companies.Count == 0) throw new ApiExceptions($"{Message.C001}");

            return Ok(new
            {
                companies
            });


        } 
        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCompanyById(long id)
        {
            var companies = await _companyRepository.GetCompanyById(id);
            
            if (companies == null) throw new ApiExceptions($"{Message.C002}");

            return Ok(new
            {
                companies.Id,
                companies.Name,
                companies.Email,
                companies.Address,
                companies.PhoneNumber,
                companies.CreatedAt
            });


        }

        #endregion
       
    }
}