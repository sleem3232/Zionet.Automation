using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Zionet.Automation.Framework.Config
{
    public class FrameworkConfig
    {
        private static string _configurationFile;
        private static XElement _xml;

        public FrameworkConfig(string configFile)
        {
            _configurationFile = configFile;
            _xml = XElement.Load(_configurationFile);
        }

        public static string GetElement(string elementName)
        {
            return _xml.Elements(elementName).First().Value;
        }

        public static string GetElement(string className, string elementName)
        {
            return _xml.Elements(className).Elements(elementName).First().Value;
        }

        public static void SetElement(string className, string elementName, string requiredValue)
        {
            _xml.Elements(className).Elements(elementName).First().Value = requiredValue;
            File.WriteAllText(_configurationFile, _xml.ToString());
        }
    }

}
