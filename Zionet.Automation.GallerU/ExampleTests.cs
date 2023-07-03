//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Zionet.Automation.Framework.Common.Enums.Auth0.Auth0Enums;
//using static Zionet.Automation.Framework.Common.Enums.GallerU.CommonEnums;
//using static Zionet.Automation.Framework.Common.Enums.GallerU.Photographer.PhotographerEnums;
//using Xunit;
//using Zionet.Automation.Framework.Config;
//using Zionet.Automation.Framework.Pages.Guest;
//using Zionet.Automation.Framework.Pages.Photographer;
//using Zionet.Automation.Framework.Services.Reporter;
//using Zionet.Automation.Framework.TestsBase;
//using Xunit.Abstractions;

//namespace Zionet.Automation.GallerU
//{
//    public class ExampleTests : BaseTest
//    {
//        //private static ConfigHelper _configHelper = new ConfigHelper($@"\.\Resources\ConfigFile.xml");

//       // C:\Users\PC\Desktop\Selenium\Zionet.Automation\Zionet.Automation.Framework\Resources
//        protected Photographer photographer = new Photographer();
//        protected Guest guest = new Guest();
//        private static ConfigHelper _configHelper = new ConfigHelper(Path.Combine(Environment.CurrentDirectory, "Resources", "ConfigFile.xml"));
//        private ChromeOptions options = new ChromeOptions();
//        private Proxy proxy = new Proxy();
//        private string selfeVideoPath = _configHelper.GetElement("SelfeVideo_Path", "GallerU");

//        public ExampleTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
//        {
//            proxy.HttpProxy = "http://localhost:56133/";
//            options.Proxy = proxy;
//            options.AddArguments("start-maximized");
//            options.AddArgument("--remote-debugging-port=6321"); // Set the desired port here
//            options.AddArguments("--use-fake-device-for-media-stream"); // Add the following arguments to enable camera access
//            options.AddArguments("--use-fake-ui-for-media-stream");
//            options.AddArgument($"--use-file-for-fake-video-capture={selfeVideoPath}/newfile.y4m");
//        }

//        //[OneTimeSetUp]
//        //public void Setup()
//        //{
//        //    var sharedFolder = $@"{_configHelper.GetElement("SharedFolder", "GallerU")}";

//        //    if (Directory.Exists(sharedFolder))
//        //    {
//        //        ReportManager.Driver("### Shared Folder Exists ###");
//        //    }

//        //    else
//        //    {
//        //        ReportManager.Driver("### Shared Folder Doesn't Exists ###");

//        //        string scriptFilePath = $@"{_configHelper.GetElement("SharedFolderSetup", "GallerU")}";

//        //        // Read the script content from the text file
//        //        string scriptContent = File.ReadAllText(scriptFilePath);

//        //        // Create a new process instance
//        //        Process process = new Process();

//        //        // Set the process StartInfo properties
//        //        process.StartInfo.FileName = "powershell.exe";
//        //        process.StartInfo.Arguments = $"-ExecutionPolicy Bypass -Command \"{scriptContent}\"";
//        //        process.StartInfo.Verb = "runas";
//        //        process.StartInfo.RedirectStandardOutput = true;
//        //        process.StartInfo.RedirectStandardError = true;
//        //        process.StartInfo.UseShellExecute = false;
//        //        process.StartInfo.CreateNoWindow = true;
//        //        // Event handlers for capturing output and errors
//        //        process.OutputDataReceived += (sender, e) =>
//        //        {
//        //            Console.WriteLine(e.Data);
//        //            ReportManager.Driver($"OUTPUT DATA RECEIVED:\t{e.Data}");
//        //        };
//        //        process.ErrorDataReceived += (sender, e) =>
//        //        {
//        //            Console.WriteLine(e.Data);
//        //            ReportManager.Driver($"ERROR DATA RECEIVED:\t{e.Data}");
//        //        };

//        //        // Start the process
//        //        process.Start();
//        //        // Begin asynchronously reading the output and error streams
//        //        process.BeginOutputReadLine();
//        //        process.BeginErrorReadLine();
//        //        // Wait for the process to exit
//        //        process.WaitForExit();
//        //        // Display exit code
//        //        if (process.ExitCode == 0)
//        //        {
//        //            ReportManager.Driver("### Shared Folder Now Exists ###");
//        //        }
//        //        else
//        //        {
//        //            ReportManager.Fatal("### Shared Folder Can't Be Exists ###");
//        //        }

//        //        // Close the process
//        //        process.Close();

//        //        ReportManager.Driver("### Shared Folder Now Exists ###");
//        //    }
//        //}

//        //[Test]
//        //public void ExampleTest()
//        //{
//        //    using (IWebDriver driver = new ChromeDriver(options))
//        //    {
//        //        ReportManager.Driver("Login Steps Start");
//        //        photographer.Login(driver, Auth0Type.Email, Login_Email.InputEmail, Login_Password.InputPassword);
//        //        Thread.Sleep(TimeSpan.FromSeconds(5));
//        //        //Assert.AreEqual(true, photographer.isAuthentication(driver)); //TODO : Need to check (Coockies not found)
//        //        photographer.AddNewEvent(driver);
//        //        photographer.GoPastEvents(driver);
//        //        photographer.OpenRecentEvent(driver);
//        //        photographer.GoHome(driver);
//        //        photographer.OpenRecentEvent(driver);
//        //        photographer.Uploadphoto(driver);
//        //        ReportManager.Driver("Login Steps End");
//        //        driver.Quit();
//        //    }
//        //}

//        //[Test]
//        //public void DeleteRecentEvent()
//        //{
//        //    using (IWebDriver driver = new ChromeDriver(options))
//        //    {
//        //        ReportManager.Driver("Login Steps Start");
//        //        photographer.Login(driver, Auth0Type.Email, Login_Email.InputEmail, Login_Password.InputPassword);
//        //        Thread.Sleep(TimeSpan.FromSeconds(5));

//        //        photographer.AddNewEvent(driver);
//        //        photographer.CatchAlert(driver, NotificationState.ReqCreate, NotificationState.CmtCreate);
//        //        Assert.AreEqual(RequestDict[NotificationAction.Create], photographer.notification.Request);
//        //        Assert.AreEqual(CommentDict[NotificationAction.Create], photographer.notification.Comment);

//        //        photographer.DeleteRecentEvent(driver);
//        //        photographer.CatchAlert(driver, NotificationState.ReqDelete, NotificationState.ReqDelete);
//        //        Assert.AreEqual(RequestDict[NotificationAction.Delete], photographer.notification.Request);
//        //        Assert.AreEqual(CommentDict[NotificationAction.Delete], photographer.notification.Comment);

//        //        ReportManager.Driver("Login Steps End");
//        //        driver.Quit();
//        //    }
//        //}

//        //[Test]
//        //public void ExampleTestEventEmpty()
//        //{
//        //    using (IWebDriver driver = new ChromeDriver(options))
//        //    {
//        //        ReportManager.Driver("Test Event Empty Start");
//        //        photographer.Login(driver, Auth0Type.Email, Login_Email.InputEmail, Login_Password.InputPassword);
//        //        Thread.Sleep(TimeSpan.FromSeconds(5));
//        //        photographer.AddNewEvent(driver, "", DateTime.Now);
//        //        Assert.AreEqual(RequestDict[NotificationAction.Create], photographer.notification.Request);
//        //        Assert.AreEqual(CommentDict[NotificationAction.Create], photographer.notification.Comment);
//        //        ReportManager.Driver("Test Event Empty End");
//        //        driver.Quit();
//        //    }
//        //}

//        //[Test]
//        //public void ExampleTestDateNull()
//        //{
//        //    using (IWebDriver driver = new ChromeDriver(options))
//        //    {
//        //        ReportManager.Driver("Test Date Null Start");
//        //        photographer.Login(driver, Auth0Type.Email, Login_Email.InputEmail, Login_Password.InputPassword);
//        //        Thread.Sleep(TimeSpan.FromSeconds(5));
//        //        photographer.AddNewEvent(driver, "Test Event Title", null);
//        //        Assert.AreEqual(RequestDict[NotificationAction.Create], photographer.notification.Request);
//        //        Assert.AreEqual(CommentDict[NotificationAction.Create], photographer.notification.Comment);
//        //        ReportManager.Driver("Test Date Null End");
//        //        driver.Quit();
//        //    }
//        //}

//        //[Test]
//        //public void CreateNewEventTest()
//        //{
//        //    using (IWebDriver driver = new ChromeDriver(options))
//        //    {
//        //        ReportManager.Driver("AddNewEvent Steps Start");
//        //        photographer.Login(driver, Auth0Type.Email, Login_Email.InputEmail, Login_Password.InputPassword);
//        //        Thread.Sleep(TimeSpan.FromSeconds(5));
//        //        photographer.AddNewEvent(driver);
//        //        Assert.AreEqual(RequestDict[NotificationAction.Create], photographer.notification.Request);
//        //        Assert.AreEqual(CommentDict[NotificationAction.Create], photographer.notification.Comment);
//        //        ReportManager.Driver("AddNewEvent Steps End");
//        //        driver.Quit();
//        //    }
//        //}

//        //[Test]
//        //public void ChangeEventList()
//        //{
//        //    using (IWebDriver driver = new ChromeDriver(options))
//        //    {
//        //        ReportManager.Driver("ChangeEventList Steps Start");
//        //        photographer.Login(driver, Auth0Type.Email, Login_Email.InputEmail, Login_Password.InputPassword);
//        //        photographer.AddNewEvent(driver);
//        //        photographer.GoUpcomingEvents(driver);
//        //        Thread.Sleep(5000);
//        //        photographer.GoPastEvents(driver);
//        //        photographer.Logout(driver);
//        //        CreateNewEventTest();

//        //        ReportManager.Driver("ChangeEventList Steps End");
//        //    }
//        //}

//        //[Test]
//        //public void ChangeEventList01()
//        //{
//        //    using (IWebDriver driver = new ChromeDriver(options))
//        //    {
//        //        ReportManager.Driver("ChangeEventList Steps Start");
//        //        photographer.Login(driver, Auth0Type.Email, Login_Email.InputEmail, Login_Password.InputPassword);
//        //        photographer.GoUpcomingEvents(driver);
//        //        Thread.Sleep(5000);
//        //        photographer.GoPastEvents(driver);

//        //        ReportManager.Driver("ChangeEventList Steps End");
//        //    }
//        //}
//        [Fact]
//        public void GeneralTest()
//        {
//            using (IWebDriver driver = new ChromeDriver(options))
//            {
//                ReportManager.Driver("Login Photographer Steps Start");
//                photographer.Login(driver, Auth0Type.Email, Login_Email.InputEmail, Login_Password.InputPassword);
//                Thread.Sleep(TimeSpan.FromSeconds(5));
//                photographer.AddNewEvent(driver);
//                photographer.GoHome(driver);
//                photographer.GoPastEvents(driver);
//                // photographer.GoUpcomingEvents(driver);
//                photographer.OpenRecentEvent(driver);
//                Thread.Sleep(5000);
//                photographer.Uploadphoto(driver);
//                photographer.EventURLButtons(driver, EventsButtons.CopyToClipBoard);

//                ReportManager.Driver("Login Guest Steps Start");
//                //photographer.GoGuestURL(driver);
//                guest.Login(driver, Auth0Type.Email, Login_Email.InputEmail, Login_Password.InputPassword, EventsButtons.CopyToClipBoard);
//                guest.OpenCamera(driver);
//                Thread.Sleep(5000);
//                guest.TakeSelfi(driver);
//                // guest.RetakePicture(driver);
//                //guest.TakeSelfi(driver);
//                guest.UseThisPicture(driver);
//                // guest.Logout(driver);
//                ReportManager.Driver("Login Guest Steps End");
//                Thread.Sleep(5000);
//                ReportManager.Driver("Login Photographer Steps End");
//                driver.Quit();
//            }
//        }

//        [Fact]
//        public void SOSO()
//        {
//            using (IWebDriver driver = new ChromeDriver(options))
//            {
//                photographer.GoGuestURL(driver);
//            }
//        }
//    }
//}
