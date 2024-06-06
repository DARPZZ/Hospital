using Moq;
using Xunit;
using Hospital.ViewModels;
using Hospital.Services;
using Hospital.Models;
using Hospital.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Tests
{
    public class OpeningViewModelTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IDrawerService> _mockDrawerService;
        private readonly Mock<IPreferencesService> _mockPreferencesService;
        private readonly OpeningViewModel _viewModel;

        public OpeningViewModelTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockDrawerService = new Mock<IDrawerService>();
            _mockPreferencesService = new Mock<IPreferencesService>();
            _mockPreferencesService.Setup(p => p.Get("drawerID", "Your id")).Returns("123");

            _viewModel = new OpeningViewModel(_mockUserService.Object, _mockDrawerService.Object, _mockPreferencesService.Object);
        }

        [Fact]
        public async Task ApplyQueryAttributes_SetsEmailAndGetsDrawerId()
        {

            var query = new Dictionary<string, object> { { "email", "test@example.com" } };
            _mockDrawerService.Setup(x => x.GetIdOfUsersDrawer("test@example.com")).ReturnsAsync(new Drawer { id = 1 });

            _viewModel.ApplyQueryAttributes(query);
            Assert.Equal("123", _viewModel.ScanText);
        }
    }
}
