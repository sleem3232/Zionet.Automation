using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zionet.Automation.Framework.Services.Reporter
{
    public interface IReporter
    {
        void Fatal(string message);
        void Fatal(string message, string fileAttachment);

        void Error(string message);
        void Error(string message, string fileAttachment);

        void Test(string message);
        void Test(string message, string fileAttachment);

        void Component(string message);
        void Component(string message, string fileAttachment);

        void Driver(string message);
        void Driver(string message, string fileAttachment);

        void TestWrapUp(string message, bool isTestFailed);
    }

}
