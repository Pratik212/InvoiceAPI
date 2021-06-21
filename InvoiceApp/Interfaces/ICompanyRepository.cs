using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceApp.Models;

namespace InvoiceApp.Interfaces
{
    public interface ICompanyRepository : ICommonRepository
    {
        Task<Company> AddCompany(Company company);

        Task<List<Company>> GetCompany();

        Task<Company> GetCompanyById(long id);
    }
}