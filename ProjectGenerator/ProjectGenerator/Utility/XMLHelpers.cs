using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectGenerator.Utility
{
    class XMLHelpers
    {
        //Formats name and value into this ' Name="value"'
        public static string CreateAttribute(String name, String value)
        {
            return " " + name + "=\"" + value + "\"";
        }

        public static string GetAttributeFieldOrNull(System.Xml.XmlNode node, string name)
        {
            System.Xml.XmlNode attribute = node.Attributes.GetNamedItem(name);
            return attribute != null ? attribute.Value : null;
        }

        public static string GetFilterPath(System.Xml.XmlElement element)
        {
            System.Xml.XmlNodeList children = element.GetElementsByTagName("Filter");
            return children.Count == 1 ? children[0].InnerText : null;
        }

        public static void CreateCompileNodesInGroup(System.Xml.XmlNode parentNode, string path)
        {
            var element = parentNode.OwnerDocument.CreateElement("ClCompile", parentNode.OwnerDocument.FirstChild.NamespaceURI);
            element.SetAttribute("Include", path);

            parentNode.AppendChild(element);
        }

        public static void CreateIncludeNodeInGroup(System.Xml.XmlNode parentNode, string path, string optionalFilter)
        {
            System.Xml.XmlElement element = parentNode.OwnerDocument.CreateElement("ClInclude", parentNode.OwnerDocument.FirstChild.NamespaceURI);
            element.SetAttribute("Include", path);

            if (optionalFilter != null)
            {
                System.Xml.XmlElement filterXMLGroup = parentNode.OwnerDocument.CreateElement("Filter", parentNode.OwnerDocument.FirstChild.NamespaceURI); ;
                filterXMLGroup.InnerText = optionalFilter;
                element.AppendChild(filterXMLGroup);
            }

            parentNode.AppendChild(element);
        }
    }
}
