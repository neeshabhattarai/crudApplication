using crudApplication.Controllers;
using crudApplication.Models;
using crudApplication.UnitTest.Model;
using Microsoft.AspNetCore.Mvc;

using Moq;

public class LoginControllerTests
{
    [Fact]
    public async Task LoginValidation()
    {
        
        var userManagerMock = IdentityMock.MockUserManager<ApplicationUser>();
        var signInManagerMock = IdentityMock.MockSignInManager<ApplicationUser>(userManagerMock.Object);

        signInManagerMock
            .Setup(s => s.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                false,
                false))
            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

        var controller = new LoginController(
            signInManagerMock.Object,
            userManagerMock.Object
        );

        var login = new Login
        {
            UserName = "testuser",
            Password = "password"
        };

     
        var result = await controller.Index(login);

        
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        Assert.Equal("Item", redirect.ControllerName);
    }
}

