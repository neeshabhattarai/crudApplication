

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace crudApplication.UnitTest.Model
{
    internal class IdentityMock
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser:class{
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(
                store.Object, null, null, null, null, null, null, null, null
            );

        }
        public static Mock<SignInManager<TUser>> MockSignInManager<TUser>(
        UserManager<TUser> userManager)
        where TUser : class
        {
            return new Mock<SignInManager<TUser>>(
                userManager,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<TUser>>(),
                null, null, null, null
            );
        }
    }
}
