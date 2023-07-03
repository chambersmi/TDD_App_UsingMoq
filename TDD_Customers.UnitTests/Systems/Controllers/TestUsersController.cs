namespace TDD_Customers.UnitTests.Systems.Controllers;

using FluentAssertions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using TDD_Customers.API.Controllers;
using TDD_Customers.API.Services;
using Moq;
using TDD_Customers.API.Models;
using TDD_Customers.UnitTests.Fixtures;

public class TestUsersController
{


    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        //Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUsersService.Object);

        //Act
        var result = (OkObjectResult) await sut.Get();

        //Assert
        result.StatusCode.Should().Be(200);
    }

    // Invoke user service
    [Fact]
    public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
    {

        //Mock user service
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        //Arrange
        var sut = new UsersController(mockUsersService.Object);

        //Act
        var result = await sut.Get();

        //Assert
        mockUsersService.Verify(service => service.GetAllUsers(), Times.Once());
    }

    [Fact]
    public async Task Get_OnSucess_ReturnsListOfUsers()
    {
        //Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUsersService.Object);

        //Act
        var result = await sut.Get();

        //Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        //Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersService.Object);

        //Act
        var result = await sut.Get();

        //Assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);

    }


}