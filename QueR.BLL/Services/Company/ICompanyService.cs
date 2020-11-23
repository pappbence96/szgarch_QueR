using QueR.BLL.Services.Company.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Company
{
    public interface ICompanyService
    {
        Task AssignAdminToCompany(int adminId, int companyId);
        Task<CompanyDto> CreateCompany(CompanyModel model);
        Task<IEnumerable<CompanyDto>> GetCompanies();
        Task RemoveAdminOfCompany(int companyId);
        Task UpdateCompany(int companyId, CompanyModel model);
    }
}