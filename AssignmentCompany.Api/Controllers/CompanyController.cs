using System;
using System.Threading.Tasks;
using AssignmentCompany.Data;
using AssignmentCompany.Repo;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentCompany.Api.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly IGenericRepository _company;

        public CompanyController(IGenericRepository company)
        {
            this._company = company;
        }

        [HttpGet]
        public async Task<IActionResult> GetListCompany()
        {
            try
            {
                var listCompany = await _company.GetAllCompanyAsync();
                return Ok(listCompany);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{globalId}")]
        public async Task<IActionResult> GetCompanyById(Guid globalId)
        {
            try
            {
                var company = await _company.GetCompanyByGlobalId(globalId);
                return Ok(company);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody]CompanyEntity company)
        {
            if (company != null)
            {
                var newCompany = new CompanyEntity
                {
                    NameCompany = company.NameCompany,
                    TitleCompany = company.TitleCompany,
                    DateCreated = DateTime.Now,
                    GlobalId = Guid.NewGuid()
                };
                try
                {
                    await _company.CreateCompanyAsync(newCompany);
                    return Ok();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return BadRequest();
        }

        [HttpPut("{globalId}")]
        public async Task<IActionResult> UpdateCompany(Guid globalId, [FromBody]CompanyEntity company)
        {
            if (company != null)
            {
                try
                {
                    var olderCompany = _company.GetCompanyByGlobalId(globalId);
                    if (olderCompany == null)
                    {
                        return new BadRequestObjectResult("Can't not search with {globalId}");
                    }
                    var newCompany = new CompanyEntity
                    {
                        NameCompany = company.NameCompany,
                        TitleCompany = company.TitleCompany,
                        DateCreated = DateTime.Now,
                        GlobalId = globalId
                    };

                    await _company.UpdateCompanyAsync(newCompany);
                    return Ok();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return BadRequest();
        }

        [HttpDelete("{globalId}")]
        public async Task<IActionResult> DeleteCompany(Guid globalId)
        {
            try
            {
                var olderCompany = _company.GetCompanyByGlobalId(globalId);
                if (olderCompany == null)
                {
                    return new BadRequestObjectResult("Can't not search with {globalId}");
                }
                await _company.DeleteCompanyAsync(globalId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}