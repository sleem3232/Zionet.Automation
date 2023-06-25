using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Zionet.Automation.Framework.Config;

namespace Zionet.Automation.Framework.Services.Reporter
{
    public class LocalLoggerReporter : IReporter
    {
        private static string TestSequenceStepsCSVFileName = "Sequence_Steps.txt";
        public static string OutputDataFolder = $@".\OutputData\{DateTime.Now:dd-MM-yyyy HH-mm-ss}";
     //   private readonly ITestOutputHelper _output;


        public static string TestOutputDataFolder = "Sequence_Steps.txt";
        public static string CurrentTestLogFile = "";

        public static DateTime StepStartTime { get; set; }
        public static string? CurrentStep { get; set; }

        /// <summary>
        /// TODO - flag control
        /// </summary>
        public static bool DoSequencesStepsReport { get; set; } = true;

        public void Component(string message)
        {
            Logger.Instance.Log(message, Logger.MESSAGE_TYPE.Component);
        }

        public void Component(string message, string fileAttachment)
        {
            if (!File.Exists(fileAttachment))
            {
                throw new IOException("File " + fileAttachment + " is not exist");
            }
            Logger.Instance.Log(message, Logger.MESSAGE_TYPE.Component);
        }

        public void Driver(string message)
        {
            Logger.Instance.Log(message, Logger.MESSAGE_TYPE.Driver);
        }

        public void Driver(string message, string fileAttachment)
        {
            if (!File.Exists(fileAttachment))
            {
                throw new IOException("File " + fileAttachment + " is not exist");
            }
            Logger.Instance.Log(message, Logger.MESSAGE_TYPE.Driver);
        }

        public void Test(string message)
        {
            Logger.Instance.Log(message, Logger.MESSAGE_TYPE.Test);

            CurrentStep = message.Replace(",", " ");

            if (!Directory.Exists(OutputDataFolder))
            {
                Directory.CreateDirectory(OutputDataFolder);
            }
            //string currentTestName = TestContext.CurrentContext.Test.Name;

         //   string currentTestName = _output.GetType().Name;


          //  TestOutputDataFolder = $@"{OutputDataFolder}\{currentTestName}";
            if (!Directory.Exists(TestOutputDataFolder))
            {
                Directory.CreateDirectory(TestOutputDataFolder);
            }

            string testStepsFile = $@"{TestOutputDataFolder}\{TestSequenceStepsCSVFileName}";
            FileStream fs;
            if (!File.Exists(testStepsFile))
            {
                fs = File.Create(testStepsFile);
                fs.Dispose();
                File.AppendAllText(testStepsFile, $@"Step,Status,Duration [min:sec:milisec],Fail Reason,ScreenShot");
                File.AppendAllText(testStepsFile, $"{Environment.NewLine}{CurrentStep},Pass,");
            }
            else if (DoSequencesStepsReport)
            {
                var testDuration = DateTime.Now - StepStartTime;
                var testDurationFormat = $@"{testDuration.Minutes}:{testDuration.Seconds}:{testDuration.Milliseconds}";
                File.AppendAllText(testStepsFile, $"{testDurationFormat},");
                File.AppendAllText(testStepsFile, $"{Environment.NewLine}{CurrentStep},Pass,");
            }

            StepStartTime = DateTime.Now;
        }

        public void Test(string message, string fileAttachment)
        {
            if (!File.Exists(fileAttachment))
            {
                throw new IOException("File " + fileAttachment + " is not exist");
            }
            Logger.Instance.Log(message + "File attachecd: " + fileAttachment, Logger.MESSAGE_TYPE.Test);
        }

        public void Error(string message)
        {
            Logger.Instance.Log(message, Logger.MESSAGE_TYPE.Driver);
        }

        public void Error(string message, string fileAttachment)
        {
            if (!File.Exists(fileAttachment))
            {
                throw new IOException("File " + fileAttachment + " is not exist");
            }
            Logger.Instance.Log(message, Logger.MESSAGE_TYPE.Driver);
        }

        public void Fatal(string message)
        {
            Logger.Instance.Log(message, Logger.MESSAGE_TYPE.Driver);
        }

        public void Fatal(string message, string fileAttachment)
        {
            if (!File.Exists(fileAttachment))
            {
                throw new IOException("File " + fileAttachment + " is not exist");
            }
            Logger.Instance.Log(message, Logger.MESSAGE_TYPE.Driver);
        }

        public void TestWrapUp(string message, bool isTestFailed)
        {
            DoSequencesStepsReport = false;

            if (isTestFailed)
            {
                //FailedTestWarpUp(message);
            }

            GenerateTestLogFile();
        }

        public static void FailedTestWarpUp(string message = "")
        {
            message = message.Replace($"{Environment.NewLine}", " ").Replace($",", " ");

            //TODO - Add option to take ScreenShots
            //string imagePath = GrabScreenShot();

            var filesToCopy = Directory.GetFiles(ReportManager.TempTestDataFolder, "*.*");
            foreach (var file in filesToCopy)
            {
                try
                {
                    File.Copy(file, $@"{TestOutputDataFolder}\{Path.GetFileName(file)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception while copy files from TempTestData to TestOutputFolder. ex:[{ex.Message}]");
                }
            }

        }

        public static void GenerateTestLogFile()
        {
            try
            {
                var logsFolder = FrameworkConfig.GetElement("Logger", "LogsFolder");
                Logger.Instance.Log($"Log Folder: {logsFolder}", Logger.MESSAGE_TYPE.Driver);
                var directory = new DirectoryInfo(logsFolder);
                var currentLog = (from f in directory.GetFiles()
                                  orderby f.LastWriteTime descending
                                  select f).First();

                var logsLine = File.ReadAllLines(currentLog.FullName).ToList();
                logsLine.Reverse();

                int testStartIndex = 0;
                for (int i = 0; i < logsLine.Count; i++)
                {
                    if (logsLine[i].ToString().Contains("Test Start"))
                    {
                        testStartIndex = i;
                        break;
                    }
                }

                if (testStartIndex == 0)
                {
                    throw new Exception("Could Not Find 'Test Start' in Log Lines");
                }

                var currentTestLog = new List<string>();
                for (int i = 0; i < testStartIndex; i++)
                {
                    currentTestLog.Add(logsLine[i].ToString());
                }

                currentTestLog.Reverse();

                string testLogFile = $@"{TestOutputDataFolder}\TestLog_{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}.txt";
                File.WriteAllLines(testLogFile, currentTestLog);
                CurrentTestLogFile = testLogFile;

            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.Message, Logger.MESSAGE_TYPE.Driver);
                Logger.Instance.Log("Can't Generate Test Log File", Logger.MESSAGE_TYPE.Driver);
            }
        }
    }

    public class Logger
    {
        public enum MESSAGE_TYPE { Component, Driver, Test, Error, Fatal };

        private static readonly Logger _instance = new Logger();
        private static StringBuilder date;
        private static StringBuilder? _logFilePath;
        private static object lockThis = new object();

        public static StringBuilder LogFilePath
        {
            get
            {
                return _logFilePath;
            }
        }
        public static Logger Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Constructor - Create a log file and initalize the class members with defualt values.
        /// </summary>
        private Logger()
        {
            SetCurrentDirectoryToTestsDllFolder();

            date = new StringBuilder();
            _logFilePath = new StringBuilder();
            date.Append(DateTime.Now.Year.ToString() + "_").Append(DateTime.Now.Month.ToString() + "_");
            date.Append(DateTime.Now.Day.ToString()).Append("_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString());

            var logsFolder = FrameworkConfig.GetElement("Logger", "LogsFolder");
            if (!Directory.Exists(logsFolder))
            {
                DirectoryInfo di = Directory.CreateDirectory(logsFolder);
            }
            _logFilePath.Append($@"{logsFolder}\{date.ToString()}.txt");
        }

        private void SetCurrentDirectoryToTestsDllFolder()
        {
            var testsDllPath = Assembly.GetExecutingAssembly().CodeBase.Substring(8); //remove 'file://'
            var testsDllFolder = Path.GetDirectoryName(testsDllPath);
            Directory.SetCurrentDirectory(testsDllFolder);
        }

        public void Log(string message, MESSAGE_TYPE type)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(DateTime.Now.Hour.ToString() + ":").Append(DateTime.Now.Minute.ToString() + ":").Append(DateTime.Now.Second.ToString() + "::\t");
            sb.Append(message);

            lock (lockThis)
            {
                try
                {
                    using (StreamWriter logStreamWriter = File.AppendText(_logFilePath.ToString()))
                    {
                        string msg;

                        switch (type)
                        {
                            case MESSAGE_TYPE.Component:
                                msg = "Component::\t" + sb.ToString();
                                logStreamWriter.WriteLine(msg);
                                Console.WriteLine(msg);
                                break;
                            case MESSAGE_TYPE.Driver:
                                msg = "Driver::\t\t" + sb.ToString();
                                logStreamWriter.WriteLine(msg);
                                Console.WriteLine(msg);
                                break;
                            case MESSAGE_TYPE.Test:
                                msg = "Test::\t\t\t" + sb.ToString();
                                logStreamWriter.WriteLine(msg);
                                Console.WriteLine(msg);
                                break;
                            case MESSAGE_TYPE.Error:
                                msg = "Error::\t" + sb.ToString();
                                logStreamWriter.WriteLine(msg);
                                Console.WriteLine(msg);
                                break;
                            case MESSAGE_TYPE.Fatal:
                                msg = "Fatal::\t" + sb.ToString();
                                logStreamWriter.WriteLine(msg);
                                Console.WriteLine(msg);
                                break;
                            default:
                                msg = "Trace::\t" + sb.ToString();
                                logStreamWriter.WriteLine(msg);
                                Console.WriteLine(msg);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
