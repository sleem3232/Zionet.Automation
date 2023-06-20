using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Zionet.Automation.Framework.Config;
using Zionet.Automation.Framework.Services.Reporter;

namespace Zionet.Automation.Framework.TestsBase
{
    public class BaseTest : IDisposable
    {
        protected FrameworkConfig _frameworkConfig;
        private DateTime _startTime;
        private StreamWriter _outputFile;
        private readonly ITestOutputHelper _testOutputHelper;
        public BaseTest(ITestOutputHelper testOutputHelper)
        {
            _frameworkConfig = new FrameworkConfig($@".\Resources\FrameworkConfiguration.xml");
            _testOutputHelper = testOutputHelper;
            _startTime = DateTime.Now;
            LocalLoggerReporter.DoSequencesStepsReport = true;

            string logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "log");
            Directory.CreateDirectory(logFolderPath); // Create the "log" folder if it doesn't exist

            string logFilePath = Path.Combine(logFolderPath, "log.txt");
            _outputFile = new StreamWriter(logFilePath, true);
            _outputFile.WriteLine($"Test Start: {_testOutputHelper.GetType().Name}");
        }


        public void Dispose()
        {
            // This method runs after the test run

            var endTime = DateTime.Now;
            var testDuration = endTime - _startTime;

            // Check if any tests failed
            if (_testOutputHelper != null)
            {
                var testFailed = testDuration.TotalMilliseconds > 1000; // Example condition for test failure

                if (testFailed)
                {
                    _testOutputHelper.WriteLine("Test Done: #############Failed#############");
                    ReportManager.TestWrapUp("Test Failed", isTestFailed: true);
                    ReportManager.Error("Test Failed due to Assertion Exception");
                }
                else
                {
                    _testOutputHelper.WriteLine("Test Done: #############Passed#############");
                    ReportManager.TestWrapUp("", isTestFailed: false);
                }
            }

          

            _testOutputHelper.WriteLine($"Test Duration: {testDuration.Hours}:{testDuration.Minutes}:{testDuration.Seconds} [hr:min:sec]");

            // Close the log file
            _outputFile.Close();
        }    
      
    }
}

