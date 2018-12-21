using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentCompany.Data;

namespace AssignmentCompany.Repo
{
    public interface IGenericRepository
    {
        Task<IEnumerable<CompanyEntity>> GetAllCompanyAsync();

        Task<CompanyEntity> GetCompanyByGlobalId(Guid globalId);

        Task CreateCompanyAsync(CompanyEntity companyEntity);

        Task UpdateCompanyAsync(CompanyEntity companyEntity);

        Task DeleteCompanyAsync(Guid globalId);
    }
}
