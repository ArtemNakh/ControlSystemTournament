using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Test
{
    public class LocationServiceTests
    {
        private readonly Mock<IRepository> _mockRepository;
        private readonly LocationService _locationService;

        public LocationServiceTests()
        {
            _mockRepository = new Mock<IRepository>();
            _locationService = new LocationService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetLocationByIdAsync_ReturnsLocation()
        {
            // Arrange
            var locationId = 1;
            var expectedLocation = new Location { Id = locationId, Name = "Test Location" };
            _mockRepository.Setup(repo => repo.GetById<Location>(locationId)).ReturnsAsync(expectedLocation);

            // Act
            var result = await _locationService.GetLocationByIdAsync(locationId);

            // Assert
            Assert.Equal(expectedLocation, result);
        }

        [Fact]
        public async Task CreateLocationAsync_CreatesLocation_WhenNotExists()
        {
            // Arrange
            var newLocation = new Location { Id = 2, Name = "New Location" };
            _mockRepository.Setup(repo => repo.GetQuery<Location>(l => l.Name == newLocation.Name))
                .ReturnsAsync(Enumerable.Empty<Location>());
            _mockRepository.Setup(repo => repo.Add(newLocation)).Returns(Task.CompletedTask);

            // Act
            var result = await _locationService.CreateLocationAsync(newLocation);

            // Assert
            Assert.Equal(newLocation, result);
        }

        [Fact]
        public async Task CreateLocationAsync_ThrowsException_WhenLocationExists()
        {
            // Arrange
            var existingLocation = new Location { Id = 1, Name = "Existing Location" };
            _mockRepository.Setup(repo => repo.GetQuery<Location>(l => l.Name == existingLocation.Name))
                .ReturnsAsync(new[] { existingLocation });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _locationService.CreateLocationAsync(existingLocation));
        }

        [Fact]
        public async Task UpdateLocationAsync_CallsUpdate()
        {
            // Arrange
            var locationToUpdate = new Location { Id = 1, Name = "Updated Location" };
            _mockRepository.Setup(repo => repo.Update(locationToUpdate)).Returns(Task.CompletedTask);

            // Act
            await _locationService.UpdateLocationAsync(locationToUpdate);

            // Assert
            _mockRepository.Verify(repo => repo.Update(locationToUpdate), Times.Once);
        }

        [Fact]
        public async Task DeleteLocationAsync_DeletesLocation_WhenExists()
        {
            // Arrange
            var locationId = 1;
            var location = new Location { Id = locationId, Name = "Location to Delete" };
            _mockRepository.Setup(repo => repo.GetById<Location>(locationId)).ReturnsAsync(location);
            _mockRepository.Setup(repo => repo.Delete<Location>(locationId)).Returns(Task.CompletedTask);

            // Act
            await _locationService.DeleteLocationAsync(locationId);

            // Assert
            _mockRepository.Verify(repo => repo.Delete<Location>(locationId), Times.Once);
        }

        [Fact]
        public async Task DeleteLocationAsync_DoesNotDelete_WhenLocationDoesNotExist()
        {
            // Arrange
            var locationId = 1;
            _mockRepository.Setup(repo => repo.GetById<Location>(locationId)).ReturnsAsync((Location)null);

            // Act
            await _locationService.DeleteLocationAsync(locationId);

            // Assert
            _mockRepository.Verify(repo => repo.Delete<Location>(locationId), Times.Never);
        }

        [Fact]
        public async Task GetAllLocations_ReturnsAllLocations()
        {
            // Arrange
            var locations = new List<Location>
            {
                new Location { Id = 1, Name = "Location 1" },
                new Location { Id = 2, Name = "Location 2" }
            };
            _mockRepository.Setup(repo => repo.GetAll<Location>()).Returns(locations);

            // Act
            var result = await _locationService.GetAllLocations();

            // Assert
            Assert.Equal(locations, result);
        }
    }
}
