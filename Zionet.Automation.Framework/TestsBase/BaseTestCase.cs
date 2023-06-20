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
    public class BaseTest  : IDisposable
    {
        protected FrameworkConfig _frameworkConfig;
        protected DateTime StartTime;
        public  ITestOutputHelper testOutputHelper;

        public  BaseTest(ITestOutputHelper testOutputHelper)
        {
            _frameworkConfig = new FrameworkConfig($@".\Resources\FrameworkConfiguration.xml");
            this.testOutputHelper= testOutputHelper;
            //TODO: Convert it to .NET7
            //ReportManager.Test($"Test Start: {testOutputHelper.}");
            //ReportManager.Test($@"Test OutputData Folder: {Dns.GetHostName()}\{Directory.GetCurrentDirectory().Replace(':', '$')}\{LocalLoggerReporter.TestOutputDataFolder.Remove(0, 2)}");
            StartTime = DateTime.Now;
            LocalLoggerReporter.DoSequencesStepsReport = true;
        }

        public void Dispose()
        {
            // This method runs after each test case
            var endTime = DateTime.Now;
            var testDuration = endTime - StartTime;

            // Any teardown code specific to your project

            Console.WriteLine($"Test Duration: {testDuration.Hours}:{testDuration.Minutes}:{testDuration.Seconds} [hr:min:sec]");
        }
    }
}
