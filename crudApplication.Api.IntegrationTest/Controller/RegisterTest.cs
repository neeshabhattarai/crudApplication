using crudApplication.Api.IntegrationTest.FakeDatabase;
using crudApplication.Api.IntegrationTest.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;

namespace crudApplication.Api.IntegrationTest.Controller
{
    public class RegisterTest:IClassFixture<CustomWebFactory<Program>>
    {
        public readonly HttpClient _httpClient;
        private readonly CustomWebFactory<Program> _customWebFactoryMsg;
        public RegisterTest(CustomWebFactory<Program> factoryMsg)
        {
            _httpClient=factoryMsg.CreateClient(); 
            _customWebFactoryMsg=factoryMsg;
        }
        [Fact]
        public async Task GetUserView()
        {
            var userView =await _httpClient.GetAsync("/api/register");
            userView.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task RegisterUser()
        {
         
            var user=await _httpClient.PostAsJsonAsync("/api/register",GetALlData(),JsonSerializerOptions.Default);
            Assert.Equal(HttpStatusCode.OK,user.StatusCode);

        }
        [Fact]
        public async Task CheckSignIn()
        {
            var _httpClient=_customWebFactoryMsg.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect=false});
            var user = await _httpClient.PostAsJsonAsync("/api/register", GetALlData(), JsonSerializerOptions.Default);
            Assert.Equal(HttpStatusCode.Found, user.StatusCode);
            //Console.WriteLine(user.Headers.Location);
            //Assert.Contains(user.Headers.Location!.ToString(),"/Otp");
            var verifyOtp = await _httpClient.PostAsJsonAsync("/api/register/Otp", new VerifyOtp { Otp = "1234" }, JsonSerializerOptions.Default);
            Assert.Equal(verifyOtp.StatusCode, HttpStatusCode.OK);

        }
        [Fact]
        public async Task CheckUserName()
        {
            var user = GetALlData().cloneWith(req => req.UserName = string.Empty);
            var request = await _httpClient.PostAsJsonAsync("/api/register", user, JsonSerializerOptions.Default);
            Assert.Equal(HttpStatusCode.BadRequest, request.StatusCode);
            var response = await request.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            Assert.Collection(response.Errors, (req) =>
            {
                Assert.Equal(req.Key, "UserName");
            });
        }
        [Theory]
        [MemberData(nameof(GetVerifiedAllTest))]
        public async Task PassALLTest(RegisterUsers users, Action<KeyValuePair<string, string[]>> validator)
        {
            var request=await _httpClient.PostAsJsonAsync("/api/register",users,JsonSerializerOptions.Default);
            var response = await request.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            Assert.Collection(response.Errors, validator);

        }
        public static RegisterUsers GetALlData()
        {
            return new RegisterUsers
            {
                Email = "test@125",
                FirstName = "testee",
                LastName = "testii",
                Password = "padkjakljfdkljd",
                PhoneNumber = "980000000",
                UserName = "testingxx22"
            };
        }
        public static IEnumerable<object[]> GetVerifiedAllTest()
        {
            return new List<object[]>
            {
                new object[]
                {
                    GetALlData().cloneWith(req=>req.UserName=null),
                    new Action<KeyValuePair<string, string[]>>
                   (req =>
                   {
                       Assert.Equal("UserName",req.Key);
                       Assert.Equal("The UserName field is required.",Assert.Single(req.Value));
                   })
                },
                new object[]
                {
                    GetALlData().cloneWith(val=>val.FirstName=null),
                    new Action<KeyValuePair<string, string[]>>(req =>
                    {
                        Assert.Equal("FirstName",req.Key);
                        Assert.Equal("The FirstName field is required.",Assert.Single(req.Value));
                    })
                }
            };
        }
    }
}
