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

        public static String GetAttributeFieldOrNull(System.Xml.XmlNode node, string name)
        {
            System.Xml.XmlNode attribute = node.Attributes.GetNamedItem(name);
            return attribute != null ? attribute.Value : null;
        }
    }
}
