using System;
using System.Threading.Tasks;
using Demo.Railway.Abstraction.Repositories.Documents;
using Demo.Railway.Abstraction.Services;
using Demo.Railway.Api.Controllers;
using Jpn.Utilities.Result.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Demo.Railway.Tests
{
    /// <summary>
    /// ...
    /// </summary>
    public class LightControllerTests
    {
        /// <summary>
        /// ...
        /// </summary>
        [Fact]
        public async Task GetGateway_ShouldReturnGateway_HappyPath()
        {
            // arrange
            var lightService = new Mock<ILightService>();
            var gatewayService = new Mock<IGatewayService>();
            var logger = new Mock<ILogger<LightController>>();

            var gatewayId = Guid.NewGuid().ToString();
            var lightId = Guid.NewGuid().ToString();

            var light = new Light {GatewayId = gatewayId, Id = lightId, Name = "LightTests"};
            var gateway = new Gateway {Id = gatewayId};

            lightService
                .Setup(s => s.GetLightAsync(It.Is<string>(id => id == lightId)))
                .ReturnsAsync(Result<Light>.Success(light));

            gatewayService
                .Setup(s => s.GetGatewayAsync(It.Is<string>(id => id == gatewayId)))
                .ReturnsAsync(Result<Gateway>.Success(gateway));

            // act
            var sut = new LightController(lightService.Object, gatewayService.Object, logger.Object);
            var actionResult = await sut.GetGateway(lightId);

            // assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(gatewayId, ((Gateway)result.Value).Id);
        }
    }
}
