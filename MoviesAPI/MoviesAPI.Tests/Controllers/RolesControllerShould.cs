using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoviesAPI.Api.Controllers;
using MoviesAPI.Application.Interfaces;
using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Tests.Controllers
{
    public class RolesControllerShould
    {
        /// <summary>
        /// Test para endpoint GetRoles()
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetRoles()
        {
            // Arrange
            var roleServiceMock = new Mock<IRoleService>();
            var controller = new RolesController(roleServiceMock.Object);

            var testRoles = new List<Role>
            {
                new() { Id = 1, Name = "Admin" },
                new() { Id = 2, Name = "Regular" }
            };

            roleServiceMock
                .Setup(service => service.GetRoles())
                .ReturnsAsync(testRoles);

            // Act
            var result = await controller.GetRoles();

            // Assert
            var returnedType = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRoles = Assert.IsType<List<Role>>(returnedType.Value);

            Assert.Equal(2, returnedRoles.Count);
            Assert.Equal("Admin", returnedRoles[0].Name);
            Assert.Equal("Regular", returnedRoles[1].Name);
            Assert.Equal(StatusCodes.Status200OK, returnedType.StatusCode);
        }

        /// <summary>
        /// Test para endpoint GetRoleById()
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetRoleById()
        {
            // Arrange
            var roleServiceMock = new Mock<IRoleService>();

            var testRole = new Role()
            {
                Id = 1, Name = "Admin"
            };

            roleServiceMock
                .Setup(service => service.GetRoleById(testRole.Id))
                .ReturnsAsync(testRole);

            var controller = new RolesController(roleServiceMock.Object);

            // Act
            var result = await controller.GetRoleById(testRole.Id);

            // Assert
            var returnedType = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRoles = Assert.IsType<Role>(returnedType.Value);

            Assert.Equal("Admin", returnedRoles.Name);
            Assert.Equal(StatusCodes.Status200OK, returnedType.StatusCode);
        }

        /// <summary>
        /// Test para endpoint GetRoleById con un Id no válido
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ReturnBadRequestForInvalidId()
        {
            // Arrange
            var roleServiceMock = new Mock<IRoleService>();
            var controller = new RolesController(roleServiceMock.Object);

            // Act
            var result = await controller.GetRoleById(0);

            // Assert
            var returnedType = Assert.IsType<BadRequestObjectResult>(result.Result);
            var message = Assert.IsType<string>(returnedType.Value);

            Assert.Equal("El id debe ser válido.", message);
            Assert.Equal(StatusCodes.Status400BadRequest, returnedType.StatusCode);
        }

        /// <summary>
        /// Test para endpoint con un Id de rol inexistente
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ReturnNotFoundForNonExistentId()
        {
            // Arrange
            var roleServiceMock = new Mock<IRoleService>();
            var controller = new RolesController(roleServiceMock.Object);

            // Act
            var result = await controller.GetRoleById(25);

            // Assert
            var returnedType = Assert.IsType<NotFoundObjectResult>(result.Result);
            var message = Assert.IsType<string>(returnedType.Value);

            Assert.Equal("El rol no existe.", message);
            Assert.Equal(StatusCodes.Status404NotFound, returnedType.StatusCode);
        }
    }
}
