using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ProjectGenerator.Utility
{
    class XMLHelpers
    {
        //Formats name and value into this ' Name="value"'
        public static string CreateAttribute(String name, String value)
        {
            return " " + name + "=\"" + value + "\"";
        }

        public static string GetAttributeFieldOrNull(XmlNode node, string name)
        {
            XmlNode attribute = node.Attributes.GetNamedItem(name);
            return attribute != null ? attribute.Value : null;
        }

        public static string GetFilterPath(XmlElement element)
        {
            XmlNodeList children = element.GetElementsByTagName("Filter");
            return children.Count == 1 ? children[0].InnerText : null;
        }

        public static void CreateCompileNodesInGroup(XmlNode parentNode, string path)
        {
            var element = parentNode.OwnerDocument.CreateElement("ClCompile", parentNode.OwnerDocument.FirstChild.NamespaceURI);
            element.SetAttribute("Include", path);

            parentNode.AppendChild(element);
        }

        public static void CreateIncludeNodeInGroup(XmlNode parentNode, string path, string optionalFilter)
        {
            XmlElement element = parentNode.OwnerDocument.CreateElement("ClInclude", parentNode.OwnerDocument.FirstChild.NamespaceURI);
            element.SetAttribute("Include", path);

            if (optionalFilter != null)
            {
                XmlElement filterXMLGroup = parentNode.OwnerDocument.CreateElement("Filter", parentNode.OwnerDocument.FirstChild.NamespaceURI); ;
                filterXMLGroup.InnerText = optionalFilter;
                element.AppendChild(filterXMLGroup);
            }

            parentNode.AppendChild(element);
        }
    }
}
