using System.Threading.Tasks;
using InvoiceApp.Interfaces;
using InvoiceApp.Models;

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
    }
}