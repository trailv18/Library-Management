using System.Threading.Tasks;
using Training.Models.TokenAuth;
using Training.Web.Controllers;
using Shouldly;
using Xunit;

namespace Training.Web.Tests.Controllers
{
    public class HomeController_Tests: TrainingWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}