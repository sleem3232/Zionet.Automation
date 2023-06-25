using System;
using Xunit.Abstractions;
using Zionet.Automation.Framework.TestsBase;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using Zionet.Automation.Framework.Pages.Photographer;
using static Zionet.Automation.Framework.Common.Enums.Auth0.Auth0Enums;
using static Zionet.Automation.Framework.Common.Enums.GallerU.Photographer.PhotographerEnums;
using Zionet.Automation.Framework.Pages.Guest;
using Zionet.Automation.Framework.Services.Reporter;
using System.Diagnostics;
using Zionet.Automation.Framework.Config;

namespace Zionet.Automation.GallerU
{
    public class GallerUTests : BaseTest
    {
        private ChromeOptions options = new ChromeOptions();
        private Proxy proxy = new Proxy();
        protected Photographer photographer = new Photographer();
        protected Guest guest = new Guest();


        public GallerUTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            proxy.HttpProxy = "http://localhost:56133/";
            options.Proxy = proxy;
            options.AddArguments("start-maximized");
            options.AddArgument("--remote-debugging-port=6321"); // Set the desired port here
        }
        [CollectionDefinition("SharedFolderCollection")]
        public class SharedFolderCollection : ICollectionFixture<SharedFolderFixture>
        {
            // This class has no code, and is never created. Its purpose is simply
            // to be the place to apply [CollectionDefinition] and all the
            // ICollectionFixture<> interfaces.
        }

        public class SharedFolderFixture : IDisposable
        {
            private readonly ITestOutputHelper _output;
            private readonly ConfigHelper _configHelper;

            public SharedFolderFixture(ITestOutputHelper output)
            {
                _output = output ?? throw new ArgumentNullException(nameof(output));
                _configHelper = new ConfigHelper($@".\Resources\ConfigFile.xml");

                var sharedFolder = $@"{_configHelper.GetElement("SharedFolder", "GallerU")}";

                if (Directory.Exists(sharedFolder))
                {
                    _output.WriteLine("### Shared Folder Exists ###");
                }
                else
                {
                    _output.WriteLine("### Shared Folder Doesn't Exist ###");

                    string scriptFilePath = $@"{_configHelper.GetElement("SharedFolderSetup", "GallerU")}";

                    // Read the script content from the text file
                    string scriptContent = File.ReadAllText(scriptFilePath);

                    // Create a new process instance
                    Process process = new Process();

                    // Set the process StartInfo properties
                    process.StartInfo.FileName = "powershell.exe";
                    process.StartInfo.Arguments = $"-ExecutionPolicy Bypass -Command \"{scriptContent}\"";
                    process.StartInfo.Verb = "runas";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    // Event handlers for capturing output and errors
                    process.OutputDataReceived += (sender, e) =>
                    {
                        Console.WriteLine(e.Data);
                        _output.WriteLine($"OUTPUT DATA RECEIVED:\t{e.Data}");
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        Console.WriteLine(e.Data);
                        _output.WriteLine($"ERROR DATA RECEIVED:\t{e.Data}");
                    };

                    // Start the process
                    process.Start();

                    // Begin asynchronously reading the output and error streams
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    // Wait for the process to exit
                    process.WaitForExit();

                    // Display exit code
                    if (process.ExitCode == 0)
                    {
                        _output.WriteLine("### Shared Folder Now Exists ###");
                    }
                    else
                    {
                        _output.WriteLine("### Shared Folder Can't Be Exists ###");
                        throw new Exception("Shared folder setup failed.");
                    }

                    // Close the process
                    process.Close();

                    _output.WriteLine("### Shared Folder Now Exists ###");
                }
            }

            public void Dispose()
            {
                // Clean up any resources if needed
            }
        }

        [Collection("SharedFolderCollection")]
        public class ExampleTests
        {
            private readonly SharedFolderFixture _fixture;

            public ExampleTests(SharedFolderFixture fixture)
            {
                _fixture = fixture;
            }

            // Rest of the test methods...
        }

        /**********SignUp***********/

        [Trait("Category", "SignUp")]
        public class SignUpTests
        {
            [Fact]
            public void SignUp_EmailExistsUperCasePassword()
            {
                // Test logic for SignUp_EmailExistsUperCasePassword
            }

            [Fact]
            public void SignUp_EmailExistsLowerCasePassword()
            {
                // Test logic for SignUp_EmailExistsLowerCasePassword
            }

            [Fact]
            public void SignUp_EmailExistsNumbersPassword()
            {
                // Test logic for SignUp_EmailExistsNumbersPassword
            }

            [Fact]
            public void SignUp_EmailExistsSpecialCharactersPassword()
            {
                // Test logic for SignUp_EmailExistsSpecialCharactersPassword
            }

            [Fact]
            public void SignUp_ExistsEmailMixPassword()
            {
                // Test logic for SignUp_ExistsEmailMixPassword
            }

        }
        [Fact]
        public void SignUp_EmailExistsUperCasePassword()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                ReportManager.Driver("Sign Up Email Exists UperCase Password Start");
                photographer.SignUp(driver, SignUp_Email.InputEmailNew, SignUp_Password.InputPasswordUperCase);
                Assert.Equal(false, photographer.isPasswordCorrect(driver));
                photographer.ContinueToLogin(driver);
                ReportManager.Driver("Sign Up Email Exists UperCase Password End");
            }
        }
        [Fact]
        public void SignUp_EmailExistsLowerCasePassword()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                ReportManager.Driver("Sign Up Email Exists LowerCase Password Start");
                photographer.SignUp(driver, SignUp_Email.InputEmailNew, SignUp_Password.InputPasswordLowerCase);
                Assert.Equal(false, photographer.isPasswordCorrect(driver));
                photographer.ContinueToLogin(driver);
                ReportManager.Driver("Sign Up Email Exists LowerCase Password End");
            }
        }
        [Fact]
        public void SignUp_EmailExistsNumbersPassword()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                ReportManager.Driver("Sign Up Email Exists Numbers Password Start");
                photographer.SignUp(driver, SignUp_Email.InputEmailNew, SignUp_Password.InputPasswordNumbers);
                Assert.Equal(false, photographer.isPasswordCorrect(driver));
                photographer.ContinueToLogin(driver);
                ReportManager.Driver("Sign Up Email Exists Numbers Password End");
            }
        }

        [Fact]
        public void SignUp_EmailExistsSpecialCharactersPassword()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                ReportManager.Driver("Sign Up Email Exists Special Characters Password Start");
                photographer.SignUp(driver, SignUp_Email.InputEmailNew, SignUp_Password.InputPasswordSpecialCharacters);
                Assert.Equal(false, photographer.isPasswordCorrect(driver));
                photographer.ContinueToLogin(driver);
                ReportManager.Driver("Sign Up Email Exists Special Characters Password End");
            }
        }

        [Fact]
        public void SignUp_ExistsEmailMixPassword()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                ReportManager.Driver("Sign Up Email Exists and Mix Password Start");
                photographer.SignUp(driver, SignUp_Email.InputEmailNew, SignUp_Password.InputPasswordMix);
                Assert.Equal(true, photographer.isPasswordCorrect(driver));
                photographer.ContinueToLogin(driver);
                Assert.Equal(true, photographer.isAuthentication(driver));
                ReportManager.Driver("Sign Up Email Exists and Mix Password End");
            }
        }
        
    }
}
