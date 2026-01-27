using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.Services;
using ClientConvertisseurV2.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.Tests
{
    [TestClass]
    public class WSServiceTests
    {
        /// <summary>
        /// Test WSService - appel OK
        /// </summary>
        [TestMethod]
        public async Task GetDevisesAsync_OK_ReturnsList()
        {
            // Arrange
            WSService service = new WSService("http://localhost:5093/api/");

            // Act
            List<Devise> devises = await service.GetDevisesAsync("devises");

            // Assert
            Assert.IsNotNull(devises);
            Assert.IsTrue(devises.Count > 0);
        }

        /// <summary>
        /// Test WSService - contrôleur inexistant
        /// </summary>
        [TestMethod]
        public async Task GetDevisesAsync_WrongController_ThrowsException()
        {
            // Arrange
            WSService service = new WSService("http://localhost:5093/api/");

            // Act + Assert
            await Assert.ThrowsExceptionAsync<Exception>(
                async () => await service.GetDevisesAsync("controleurinexistant")
            );
        }

        /// <summary>
        /// Test WSService - API indisponible
        /// </summary>
        [TestMethod]
        public async Task GetDevisesAsync_WrongBaseUrl_ThrowsException()
        {
            // Arrange
            WSService service = new WSService("http://localhost:9999/api/");

            // Act + Assert
            await Assert.ThrowsExceptionAsync<Exception>(
                async () => await service.GetDevisesAsync("devises")
            );
        }
    }
}
