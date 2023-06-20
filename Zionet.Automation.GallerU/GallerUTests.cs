using Xunit.Abstractions;
using Xunit;
using Zionet.Automation.Framework.TestsBase;
using OpenQA.Selenium;
using WebDriverManager;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using Xunit.Sdk;

namespace Zionet.Automation.GallerU
{
    public class GallerUTests : BaseTest
    {
        private readonly IWebDriver chromeDriver;

        public GallerUTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var driver = new DriverManager().SetUpDriver(new ChromeConfig());
            chromeDriver = new ChromeDriver();
        }

        [Fact]
        public void Test1()
        {
            // Perform your test actions
            chromeDriver.Navigate().GoToUrl("http://eaapp.somee.com");

            // Assert your test results
            Assert.True(true, "This is a sample assertion");

            // Write additional output
           
        }
    }

}
