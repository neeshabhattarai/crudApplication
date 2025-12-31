using crudApplication.Api.IntegrationTest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace crudApplication.Api.IntegrationTest.Controller
{
    public class LoginChecks : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient httpClient;
        public LoginChecks(WebApplicationFactory<Program> factory)
        {
            httpClient = factory.CreateClient();
        }
        [Fact]
        public async Task PathChecker()
        {
            var test = await httpClient.GetAsync("/api/Login");
            test.EnsureSuccessStatusCode();
        }
        [Theory]
        [MemberData(nameof(GetALLSignUpTest))]
        public async Task ValidationChecker(LoginModel modle, Action<KeyValuePair<string, string[]>> validation)
        {
            var request =await httpClient.PostAsJsonAsync("/api/Login", modle, JsonSerializerOptions.Default);
            var response = await request.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            Assert.Collection(response.Errors, validation);
        }
        
        [Fact]
        public async Task PostData_DummyData()
        {
            var user = GetData().cloneWith(req =>
            {
                req.UserName = null;
            });

            var request = await httpClient.PostAsJsonAsync("/api/Login", user, JsonSerializerOptions.Default);

            var response = await request.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            Assert.Collection(response.Errors, (req) =>
            {
                Assert.Equal("UserName", req.Key);
                Assert.Equal("The UserName field is required.", Assert.Single(req.Value));
            });



        }

        public static LoginModel GetData()
        {
            return new LoginModel { UserName = "test@12345", Password = "Test1234" };
        }

        public static IEnumerable<object[]> GetALLSignUpTest()
        {
            return new List<object[]>
            {
                new object[]
                {
                    GetData().cloneWith(req=>req.UserName=string.Empty),new Action<KeyValuePair<string, string[]>>(req =>
                    {
                        Assert.Equal("UserName",req.Key);
                        Assert.Equal("The UserName field is required.",Assert.Single(req.Value));
                    })
                },
                new object[]
                {
                    GetData().cloneWith(req=>req.Password=string.Empty),new Action<KeyValuePair<string, string[]>>(req =>
                    {
                        Assert.Equal("Password",req.Key);
                        Assert.Equal("The Password field is required.",Assert.Single(req.Value));
                    })
                }
            };

        }


    }
}
    


