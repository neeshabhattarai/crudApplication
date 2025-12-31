namespace crudApplication.Api.IntegrationTest.Model
{
    public interface IMessageService
    {
        Task sendAsync(IdentityMessage message);
    }
}
