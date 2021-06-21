using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceApp.Interfaces;
using InvoiceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Providers
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly InvoiceContext _context;

        public CompanyRepository(InvoiceContext context)
        {
            _context = context;
        }
        
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Company> AddCompany(Company company)
        {
            var companyObj = await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return companyObj.Entity;
        }

        public async Task<List<Company>> GetCompany()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyById(long id)
        {
            var companies = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            return companies;
        }
    }
}