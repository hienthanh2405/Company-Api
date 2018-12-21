using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentCompany.Data;

namespace AssignmentCompany.Repo
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IGraphRepository _graphRepository;

        public GenericRepository(IGraphRepository graphRepository)
        {
            _graphRepository = graphRepository;
        }


        public async Task<IEnumerable<CompanyEntity>> GetAllCompanyAsync()
        {
            var query = _graphRepository.GraphClient.Cypher
                .Match($"(c:{CompanyEntity.LABEL_COMPANY})")
                .Return(c => c.As<CompanyEntity>());

            var result = await query.ResultsAsync;
            return result.ToList();
        }

        public async Task<CompanyEntity> GetCompanyByGlobalId(Guid globalId)
        {
            var query = _graphRepository.GraphClient.Cypher
                .Match($"(c:Company)")
                .Where((CompanyEntity c) => c.GlobalId == globalId)
                .Return(c => c.As<CompanyEntity>());

            var result = await query.ResultsAsync;
            return result.FirstOrDefault();
        }

        public async Task CreateCompanyAsync(CompanyEntity companyEntity)
        {
            var query = _graphRepository.GraphClient.Cypher
                .Create(
                    $"(c:{CompanyEntity.LABEL_COMPANY} {{{nameof(CompanyEntity.TitleCompany)}:$TitleCompany,{nameof(CompanyEntity.NameCompany)}:$NameCompany,{nameof(CompanyEntity.GlobalId)}:$GlobalId,{nameof(CompanyEntity.DateCreated)}:$DateCreated}})")
                .WithParam("TitleCompany", companyEntity.TitleCompany)
                .WithParam("NameCompany", companyEntity.NameCompany)
                .WithParam("GlobalId", companyEntity.GlobalId)
                .WithParam("DateCreated", companyEntity.DateCreated)
                .Return(c => c.As<CompanyEntity>());
            var result = await query.ResultsAsync;
        }
 
        public async Task UpdateCompanyAsync(CompanyEntity companyEntity)
        {
            var query = _graphRepository.GraphClient.Cypher
                .Match($"(c:{CompanyEntity.LABEL_COMPANY})")
                .Where((CompanyEntity c) => c.GlobalId == companyEntity.GlobalId)
                .Set($"c = {{{nameof(CompanyEntity.TitleCompany)}:$TitleCompany,{nameof(CompanyEntity.NameCompany)}:$NameCompany,{nameof(CompanyEntity.GlobalId)}:$GlobalId,{nameof(CompanyEntity.DateCreated)}:$DateCreated}}")
                .WithParam("TitleCompany", companyEntity.TitleCompany)
                .WithParam("NameCompany", companyEntity.NameCompany)
                .WithParam("GlobalId", companyEntity.GlobalId)
                .WithParam("DateCreated", companyEntity.DateCreated)
                .Return(c => c.As<CompanyEntity>());

            var result = await query.ResultsAsync;
        }

        public async Task DeleteCompanyAsync(Guid globalId)
        {
            var query = _graphRepository.GraphClient.Cypher
                .Match($"((c:Company))")
                .Where((CompanyEntity c) => c.GlobalId == globalId)
                .DetachDelete("c");

            await query.ExecuteWithoutResultsAsync();
        }
    }
}
