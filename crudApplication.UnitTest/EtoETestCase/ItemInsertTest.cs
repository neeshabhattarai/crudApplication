
//using Microsoft.Playwright;
//using System;
//using System.Collections.Generic;
//using System.Text;


//namespace crudApplication.UnitTest.EtoETestCase
//{
//    public class ItemInsertTest
//    {

//        [Fact]
//        public async Task InsertItemForm()
//        {
//            var playWright = await Playwright.CreateAsync();
//            await using var PlayLaunch=await playWright.Chromium.LaunchAsync();
//            var Page = await PlayLaunch.NewPageAsync();
//          await  Page.GotoAsync("http://localhost:5112/Admin/Create");
//            await Page.FillAsync("#Name", "Apple");
//            await Page.FillAsync("#Description", "Testing");
//            await Page.ClickAsync("button#itembtn");
//            //await Page.WaitForURLAsync("http://localhost:5112/Admin");
//        }

//    }
//}
