using NowPlayingMonitor.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NowPlayingMonitor
{
    public static class ConfigUtil
    {
        private static string _configFilePath = PathUtil.GetConfigFilePath();

        public static string ConfigFilePath { get => _configFilePath;}

        public static bool? ReadBool(string section, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key))
                    return null;
                var stringValue = Read(section, key);
                if (stringValue != null)
                {
                    bool boolValue;
                    if (bool.TryParse(stringValue, out boolValue))
                    {
                        return boolValue;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading XML: {ex.Message}");
                return null;
            }
        }

        public static int? ReadInt(string section, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key))
                    return null;
                var stringValue = Read(section, key);
                if (stringValue != null)
                {
                    int intValue;
                    if (int.TryParse(stringValue, out intValue))
                    {
                        return intValue;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading XML: {ex.Message}");
                return null;
            }
        }

        public static string? Read(string section, string key)
        {
            try
            {
                if(string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key)) 
                    return null;
                var keyElement = FindElement(_configFilePath, section, key);
                return keyElement?.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading XML: {ex.Message}");
                return null;
            }
        }

        public static void WriteBool(string section, string key, bool value)
        {
            try
            {
                string stringValue = value.ToString();
                Write(section, key, stringValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing XML: {ex.Message}");
            }
        }

        public static void Write(string section, string key, string value)
        {
            try
            {
                var doc = LoadOrCreateDocument();
                var keyElement = FindOrCreateElement(doc, section, key);

                keyElement.Value = value;
                doc.Save(_configFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing XML: {ex.Message}");
            }
        }

        private static XDocument LoadOrCreateDocument()
        {
            if (File.Exists(_configFilePath))
            {
                return XDocument.Load(_configFilePath);
            }
            else
            {
                var doc = new XDocument(new XElement("Configuration"));
                doc.Save(_configFilePath);  
                return doc;
            }
        }

        private static XElement? FindElement(string filePath, string section, string key)
        {
            var doc = XDocument.Load(filePath);
            var root = doc.Element("Configuration");
            var sectionElement = root?.Element(section);
            return sectionElement?.Element(key);
        }

        private static XElement FindOrCreateElement(XDocument doc, string section, string key)
        {
            var root = EnsureRoot(doc);
            var sectionElement = root.Element(section) ?? CreateSection(root, section);
            return sectionElement.Element(key) ?? CreateKey(sectionElement, key);
        }

        private static XElement EnsureRoot(XDocument doc)
        {
            var root = doc.Element("Configuration");
            if (root == null)
            {
                root = new XElement("Configuration");
                doc.Add(root);
            }
            return root;
        }

        private static XElement CreateSection(XElement root, string section)
        {
            var sectionElement = new XElement(section);
            root.Add(sectionElement);
            return sectionElement;
        }

        private static XElement CreateKey(XElement sectionElement, string key)
        {
            var keyElement = new XElement(key);
            sectionElement.Add(keyElement);
            return keyElement;
        }
    }
}
