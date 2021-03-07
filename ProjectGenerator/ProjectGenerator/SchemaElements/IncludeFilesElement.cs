using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ProjectGenerator.Utility;

namespace ProjectGenerator.SchemaElements
{
    //Unique element that exists in the template file, when it reads from template it populates it, then on export it uses the data to generate dynamic info in the export
    class IncludeFilesElement : GetFilesElement
    {
        public IncludeFilesElement() : base()
        {
            SearchPatternArg = "*.h";
        }

        public override void CreateDataXMLNodesInElement(System.Xml.XmlNode parentNode)
        {
            foreach (string s in m_FilePathsFromQuery)
            {
                var element = parentNode.OwnerDocument.CreateElement("ClInclude", parentNode.OwnerDocument.FirstChild.NamespaceURI);
                element.SetAttribute("Include", s);

                parentNode.AppendChild(element);
            }
        }
    }
}
