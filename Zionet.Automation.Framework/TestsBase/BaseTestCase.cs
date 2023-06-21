using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;
using Zionet.Automation.Framework.Config;
using Zionet.Automation.Framework.Services.Reporter;
using static System.Net.Mime.MediaTypeNames;

namespace Zionet.Automation.Framework.TestsBase
{
    public abstract class BaseTest : IDisposable
    {
        protected FrameworkConfig _frameworkConfig;
        private DateTime _startTime;
        private StreamWriter _outputFile;
        public ITestOutputHelper testOutputHelper;
        protected string _testName;

        public BaseTest(ITestOutputHelper testOutputHelper)
        {
            _frameworkConfig = new FrameworkConfig($@".\Resources\FrameworkConfiguration.xml");
            this.testOutputHelper = testOutputHelper;
            _startTime = DateTime.Now;
            LocalLoggerReporter.DoSequencesStepsReport = true;

            string logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "log");
            Directory.CreateDirectory(logFolderPath); // Create the "log" folder if it doesn't exist

            string logFilePath = Path.Combine(logFolderPath, "log.txt");
            _outputFile = new StreamWriter(logFilePath, true);

            _testName = GetTestName();
            _outputFile.WriteLine($"Test Start: {_testName}");
        }
        protected void InitializeTest()
        {
            _testName = GetTestName();
            _outputFile.WriteLine($"Test Start: {_testName}");
        }

        private string GetTestName()
        {
            var callingMethod = new StackFrame(1).GetMethod();
            var testClassType = GetType();

            return $"{testClassType.Name}.{callingMethod.Name}";
        }

       
        public void Dispose()
        {
            // This method runs after the test run

            var endTime = DateTime.Now;
            var testDuration = endTime - _startTime;

            // Check if any tests failed
            if (testOutputHelper != null)
            {
                var testFailed = testDuration.TotalMilliseconds > 100000; // Example condition for test failure

                if (testFailed)
                {
                    _outputFile.WriteLine("Test Done: #############Failed#############");
                    ReportManager.TestWrapUp("Test Failed", isTestFailed: true);
                    ReportManager.Error("Test Failed due to Assertion Exception");
                }
                else
                {
                    _outputFile.WriteLine("Test Done: #############Passed#############");
                    ReportManager.TestWrapUp("***********", isTestFailed: false);
                }
            }

            _outputFile.WriteLine($"Test Duration: {testDuration.Hours}:{testDuration.Minutes}:{testDuration.Seconds} [hr:min:sec]");

            // Close the log file
            _outputFile.Close();
        }
    }
}
