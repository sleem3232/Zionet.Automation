using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Zionet.Automation.Framework.Config
{
    public class ConfigHelper
    {
        private string _configurationFile;
        private XElement _xml;

        public ConfigHelper(string configFile)
        {
            _configurationFile = configFile;
            _xml = XElement.Load(_configurationFile);
        }

        public string GetElement(string elementName, string section)
        {
            var value = _xml.Descendants(section).Where(el => el.Attribute("name")?.Value == elementName).First().Attribute("value").Value; return value;
        }

        public void SetElement(string requiredValue, string elementName, string section)
        {
            _xml.Descendants(section).Where(el => el.Attribute("name")?.Value == elementName).First().Attribute("value").Value = requiredValue;
            File.WriteAllText(_configurationFile, _xml.ToString());
        }
    }

}
