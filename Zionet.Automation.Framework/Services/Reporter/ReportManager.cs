using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zionet.Automation.Framework.Services.Reporter
{
    public static class ReportManager
    {
        private static readonly List<IReporter> _reports = new List<IReporter>()
        {
            new LocalLoggerReporter()
        };

        public readonly static string TempTestDataFolder = @"C:\temp\TempTestData";

        public static void Component(string message)
        {
            string className = new StackTrace(1).GetFrame(0).GetMethod().DeclaringType.Name;
            _reports.ForEach(report => report.Component($"{className} : {message}"));
        }

        public static void Component(string message, string fileAttachment)
        {
            string className = new StackTrace(1).GetFrame(0).GetMethod().DeclaringType.Name;
            _reports.ForEach(report => report.Component($"{className} : {message}", fileAttachment));
        }

        public static void Error(string message)
        {
            string className = new StackTrace(1).GetFrame(0).GetMethod().DeclaringType.Name;
            _reports.ForEach(report => report.Error($"{className} : {message}"));
        }

        public static void Error(string message, string fileAttachment)
        {
            string className = new StackTrace(1).GetFrame(0).GetMethod().DeclaringType.Name;
            _reports.ForEach(report => report.Error($"{className} : {message}", fileAttachment));
        }

        public static void Fatal(string message)
        {
            string className = new StackTrace(1).GetFrame(0).GetMethod().DeclaringType.Name;
            _reports.ForEach(report => report.Fatal($"{className} : {message}"));
            throw new Exception(message);
        }

        public static void Fatal(string message, string fileAttachment)
        {
            string className = new StackTrace(1).GetFrame(0).GetMethod().DeclaringType.Name;
            _reports.ForEach(report => report.Fatal($"{className} : {message}", fileAttachment));
            throw new Exception(message);
        }

        public static void Test(string message)
        {
            _reports.ForEach(report => report.Test($"{message}"));
        }

        public static void Test(string message, string fileAttachment)
        {
            _reports.ForEach(report => report.Test($"{message}", fileAttachment));
        }

        public static void Driver(string message)
        {
            string className = new StackTrace(1).GetFrame(0).GetMethod().DeclaringType.Name;
            _reports.ForEach(report => report.Driver($"{className} : {message}"));
        }

        public static void Driver(string message, string fileAttachment)
        {
            string className = new StackTrace(1).GetFrame(0).GetMethod().DeclaringType.Name;
            _reports.ForEach(report => report.Driver($"{className} : {message}", fileAttachment));
        }

        public static void TestWrapUp(string message, bool isTestFailed)
        {
            _reports.ForEach(reports => reports.TestWrapUp(message, isTestFailed));
        }
    }
}
