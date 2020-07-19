using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YTL
{
    static class SettingImporter
    {
        public static Setting getSettingFromXML(string filename)
        {
            Setting setting;
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            XmlNodeList nodes = document.GetElementsByTagName("setting");
            string url = "";
            string token = "";
            string path = "";

            foreach (XmlNode node in nodes)
            {
                 url = node.SelectSingleNode("url").InnerText;
                 token = node.SelectSingleNode("token").InnerText;
                 path = node.SelectSingleNode("path").InnerText;
            }

            setting = new Setting(url, token, path);
            return setting;
        }
    }
}
