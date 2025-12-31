//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.Playwright;
//using Microsoft.Playwright.NUnit;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace crudApplication.UnitTest.EtoETestCase
//{
//    [Parallelizable(ParallelScope.Self)]
//    public class FirstTestCase:PageTest
//    {
//        [Test]
//       public async Task SignInTest()
//        {
//            await Page.GotoAsync("http://localhost:5112/api/Login");
//            var formButton = Page.Locator("text=Open Contact Form");
//            await formButton.ClickAsync();
//            await Expect(Page).ToHaveURLAsync(new Regex(".*Home/Form"));
//            //using var playwright = await Playwright.CreateAsync();
//            //await using var browser = await playwright.Chromium.LaunchAsync(new (){
//            //    Headless=false

//            //});
//            //var page=await browser.NewPageAsync();

//            //await page.GotoAsync("http://localhost:5112/api/Login");
//            //await page.FillAsync("#UserName", "test@1234");
//            //await page.FillAsync("#Password", "Test1234");
//            //await page.ClickAsync("button[type='submit']");
//            //await page.WaitForURLAsync("**/Item");
//            //Assert.Contains("/Item", page.Url);

//        }
//    }
//}
