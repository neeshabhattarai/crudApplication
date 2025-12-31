//using Microsoft.Playwright;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace crudApplication.UnitTest.EtoETestCase
//{
//    public class SecondTestCase
//    {
//        [Fact]
//        public async Task Sigup()
//        {
//            var playwright= await Playwright.CreateAsync();
//            await using var PlaywrightSetup = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
//            {
//                Headless = true,
//            });
//            var newPageLoad=await PlaywrightSetup.NewPageAsync();
//            await newPageLoad.GotoAsync("http://localhost:5112/api/Login");
//            await newPageLoad.FillAsync("#UserName", "test@12345");
//            await newPageLoad.FillAsync("#Password", "Test123");
//            await newPageLoad.ClickAsync("button[type='submit']");
//            await newPageLoad.WaitForURLAsync("**/Admin/Index");
//            Assert.Contains("/Admin/Index", newPageLoad.Url);
            
//        }
//        [Fact]
//        public async Task Registration()
//        {
//            var playwright=await Playwright.CreateAsync();
//            await using var PlaywrightSetup=await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
//            {
//                Headless = true,
//            });
//            var Page=await PlaywrightSetup.NewPageAsync();

//            await Page.GotoAsync("http://localhost:5112/api/Register");
//            await Page.FillAsync("#FirstName", "testing");
//            await Page.FillAsync("#Email", "testing@test");

//            await Page.FillAsync("#LastName", "test");
//            await Page.FillAsync("#UserName", "testing3445");
//            await Page.FillAsync("#PhoneNumber", "9876000");
//            await Page.FillAsync("#Password", "alpha123");
//            await Page.ClickAsync("button[type='submit']");
//            // await Page.WaitForURLAsync("**/Otp");
//            //await Page.WaitForSelectorAsync("#Otp");
//            // //await Page.WaitForSelectorAsync("#Otp");
//            // //Assert.True();
//        }

//    }
//}
