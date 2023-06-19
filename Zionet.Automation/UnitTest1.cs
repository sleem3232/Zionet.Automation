using System.Collections.Generic;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using Xunit.Abstractions;
using Xunit.Sdk;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Zionet.Automation
{
    public class UnitTest1
    {
        private ITestOutputHelper testOutputHelper;
        private readonly IWebDriver chromeDriver;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            var driver = new DriverManager().SetUpDriver(new ChromeConfig());
            chromeDriver = new ChromeDriver();
        }

        [Fact]
        public void Test1()
        {
            Console.WriteLine("First test");
            testOutputHelper.WriteLine("First test");
            chromeDriver.Navigate().GoToUrl("http://eaapp.somee.com");
        }
    }
}