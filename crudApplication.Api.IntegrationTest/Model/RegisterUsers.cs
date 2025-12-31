using System.ComponentModel.DataAnnotations;

namespace crudApplication.Api.IntegrationTest.Model
{
    public class RegisterUsers
    {
        public String UserName { get; set; } = "";
        public String FirstName { get; set; } = "";
        public String LastName { get; set; } = "";
        public String Password { get; set; } = "";
        public String Email { get; set; } = "";
        public String PhoneNumber { get; set; } = "";

       public RegisterUsers cloneWith(Action<RegisterUsers> changes)
        {
            var user = (RegisterUsers)MemberwiseClone();
            changes(user);
            return user;
        }
    }
}
