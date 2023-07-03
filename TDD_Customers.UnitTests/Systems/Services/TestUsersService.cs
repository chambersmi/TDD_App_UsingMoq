using FluentAssertions;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_Customers.API.Models;
using TDD_Customers.UnitTests.Fixtures;
using TDD_Customers.UnitTests.Helpers;

namespace TDD_Customers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        // Set up message request handler to control what response comes back
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object); //controls behavior
            var sut = new UsersService(httpClient);


            //Act
            await sut.GetAllUsers();


            //Assert
            //Verify HTTP request is made
            handlerMock.Protected()
                .Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object); //controls behavior
            var sut = new UsersService(httpClient);

            //Act
            //If a 404 occurs from this endpoint, return an empty list of users to the controller.            
            var result = await sut.GetAllUsers();

            //Assert
            //Verify HTTP request is made
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object); //controls behavior
            var sut = new UsersService(httpClient);

            //Act
            //If a 404 occurs from this endpoint, return an empty list of users to the controller.            
            var result = await sut.GetAllUsers();

            //Assert
            //Verify HTTP request is made
            result.Count.Should().Be(expectedResponse.Count);
        }
    }
}
