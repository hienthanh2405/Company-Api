using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AssignmentCompany.Api.Controllers;
using AssignmentCompany.Repo;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AssignmentCompany.Test.Controllers
{
    public class CompanyControllerShould
    {
        [Fact]
        public async Task Get_List_Company_Success()
        {
            //Arrange
            var mockCompanyService = new Mock<IGenericRepository>();

            var controller = new CompanyController(mockCompanyService.Object);

            //Act
            IActionResult result = await controller.GetListCompany();
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Get_List_Company_NotConnectDatabase_Fail()
        {
            //Arrage
            var mockCompanyService = new Mock<IGenericRepository>();
            var controller = new CompanyController(mockCompanyService.Object);

            //Act
            IActionResult result = await controller.GetListCompany();
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(400, okResult.StatusCode);
        }
    }
}
