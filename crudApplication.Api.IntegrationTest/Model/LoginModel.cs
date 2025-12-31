using Microsoft.AspNetCore.Mvc;

namespace crudApplication.Api.IntegrationTest.Model
{
    public class LoginModel
    {
        public String UserName { get; set; } = "";
        public String Password { get; set; } =string.Empty;
        public LoginModel cloneWith(Action<LoginModel> changes)
        {
            var user = (LoginModel)MemberwiseClone();
            changes(user);
            return user;
        }
    }
}
