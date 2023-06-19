using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;
using Xunit.Abstractions;
using Zionet.Automation.Framework.TestsBase;

namespace Zionet.Automation.GallerU.Tests
{
    public class GallerUTests : BaseTest
    {
        private ITestOutputHelper testOutputHelper;
        private readonly IWebDriver chromeDriver;

        public GallerUTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            var driver = new DriverManager().SetUpDriver(new ChromeConfig());
            chromeDriver = new ChromeDriver();
        }
        [Fact]
        public void Test1()
        {
            testOutputHelper.WriteLine("First test");

            // Perform your test actions
            chromeDriver.Navigate().GoToUrl("http://eaapp.somee.com");

            // Assert your test results
            Assert.True(true, "This is a sample assertion");

            // Write additional output
            testOutputHelper.WriteLine("Test completed successfully");
        }
    }

}
