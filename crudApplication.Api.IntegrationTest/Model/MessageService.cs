
namespace crudApplication.Api.IntegrationTest.Model
{
    public class MessageService : IMessageService
    {
        public string LastOtp { get; set; }

        public Task sendAsync(IdentityMessage message)
        {
            LastOtp = message.Body.Split(":").Last(); 
            return Task.CompletedTask;
        }

    }
}
