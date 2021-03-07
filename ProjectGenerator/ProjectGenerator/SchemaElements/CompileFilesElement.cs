using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ProjectGenerator.Utility;

namespace ProjectGenerator.SchemaElements
{
    class CompileFilesElement : GetFilesElement
    {
        public CompileFilesElement() : base()
        {
            SearchPatternArg = "*.cpp";
        }

        public override void CreateDataXMLNodesInElement(System.Xml.XmlNode parentNode)
        {
            foreach (string s in m_FilePathsFromQuery)
            {
                var element = parentNode.OwnerDocument.CreateElement("ClCompile", parentNode.OwnerDocument.FirstChild.NamespaceURI);
                element.SetAttribute("Include", s);

                parentNode.AppendChild(element);
            }
        }
    }
}