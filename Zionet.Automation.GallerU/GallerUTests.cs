using System;
using Xunit.Abstractions;
using Zionet.Automation.Framework.TestsBase;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Zionet.Automation.GallerU
{
    public class GallerUTests : BaseTest
    {
        private ChromeOptions options = new ChromeOptions();
        private Proxy proxy = new Proxy();

        public GallerUTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            proxy.HttpProxy = "http://localhost:56133/";
            options.Proxy = proxy;
            options.AddArguments("start-maximized");
            options.AddArgument("--remote-debugging-port=6321"); // Set the desired port here
        }
      
        [Fact]
        public void Test1()
        {
            using (IWebDriver driver = new ChromeDriver(options))
            {

                testOutputHelper.WriteLine("First test");

                // Perform your test actions
                // driver.Navigate().GoToUrl("http://eaapp.somee.com");

                // Assert your test results
                // Assert.True(true, "This is a sample assertion");

                // Write additional output
                testOutputHelper.WriteLine("Test completed successfully");

                driver.Quit();
            }
        }
        [Fact]
        public void Test2()
        {
            using (IWebDriver driver = new ChromeDriver(options))
            {
                testOutputHelper.WriteLine("First test");

                // Perform your test actions
                // driver.Navigate().GoToUrl("http://eaapp.somee.com");

                // Assert your test results
                // Assert.True(true, "This is a sample assertion");

                // Write additional output
                testOutputHelper.WriteLine("Test completed successfully");

                driver.Quit();
            }
        }
    }
}
