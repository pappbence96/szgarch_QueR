using QueR.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Company
{
    public interface ICompanyService
    {
        Task AssignAdminToCompany(int adminId, int companyId);
        Task<int> CreateCompany(CompanyModel model);
        IEnumerable<Domain.Entities.Company> GetCompanies();
        Task RemoveAdminOfCompany(int companyId);
        Task UpdateCompany(int companyId, CompanyModel model);
    }
}