using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoviesAPI.Api.Controllers;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Tests.Controllers
{
    public class UserControllerShould
    {
        /// <summary>
        /// Test para endpoint RegisterUser con usuario nulo
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ReturnBadRequestForNullUser()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var controller = new UsersController(userServiceMock.Object);
            User? testUser = null;

            // Act
            var result = await controller.RegisterUser(testUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint RegisterUser con nombre de usuario existente
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ReturnConflictForExistentUser()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var controller = new UsersController(userServiceMock.Object);

            var testUser = new User
            {
                Id = 1,
                Username = "existingUser",
                Email = "test@example.com",
                Password = "123456",
                RoleId = 1
            };

            userServiceMock
                .Setup(service => service.SearchUserByUsername(testUser.Username))
                .ReturnsAsync(testUser);

            // Act
            var result = await controller.RegisterUser(testUser);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
            var message = Assert.IsType<string>(conflictResult.Value);

            Assert.Equal("El nombre de usuario o correo ya existen.", message);
            Assert.Equal(StatusCodes.Status409Conflict, conflictResult.StatusCode);
        }

        /// <summary>
        /// Test para endpoint RegisterUser para nuevo usuario
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ReturnOkForNewUser()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var controller = new UsersController(userServiceMock.Object);

            var testUser = new User
            {
                Id = 0,
                Username = "newUser",
                Email = "new@example.com",
                Password = "123456",
                RoleId = 1
            };

            var createdUser = new User
            {
                Id = 99,
                Username = testUser.Username,
                Email = testUser.Email,
                Password = testUser.Password,
                RoleId = testUser.RoleId
            };

            // No existe ni el username ni el email
            userServiceMock
                .Setup(service => service.SearchUserByUsername(testUser.Username))
                .ReturnsAsync((User)null);

            userServiceMock
                .Setup(service => service.SearchUserByEmail(testUser.Email))
                .ReturnsAsync((User)null);

            // Simular que el usuario fue creado
            userServiceMock
                .Setup(service => service.RegisterUser(testUser))
                .ReturnsAsync(createdUser);

            // Act
            var result = await controller.RegisterUser(testUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<User>(okResult.Value);

            Assert.Equal(createdUser.Id, returnedUser.Id);
            Assert.Equal("newUser", returnedUser.Username);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

    }
}
