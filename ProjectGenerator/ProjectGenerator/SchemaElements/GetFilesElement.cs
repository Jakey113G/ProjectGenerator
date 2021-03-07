using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ProjectGenerator.Utility;

namespace ProjectGenerator.SchemaElements
{
    abstract class GetFilesElement : ISchemaElement
    {
        public string DirectoryArg { get; set; }
        public string SearchPatternArg { get; set; }
        public SearchOption SearchOptionArg { get; set; }

        protected List<string> m_FilePathsFromQuery;

        public GetFilesElement()
        {
            m_FilePathsFromQuery = new List<string>();
            DirectoryArg = Directory.GetCurrentDirectory();
            SearchOptionArg = SearchOption.TopDirectoryOnly;
        }

        public void LoadPropertiesFromXML(System.Xml.XmlNode node)
        {
            DirectoryArg = XMLHelpers.GetAttributeFieldOrNull(node, "Directory") ?? DirectoryArg;
            SearchPatternArg = XMLHelpers.GetAttributeFieldOrNull(node, "SearchPattern") ?? SearchPatternArg;

            Enum.TryParse(node.Attributes.GetNamedItem("SearchOption")?.Value, out SearchOption SearchOptionArg);

            foreach (string s in Directory.GetFiles(DirectoryArg, SearchPatternArg, SearchOptionArg))
            {
                m_FilePathsFromQuery.Add(s);
            }
        }

        public abstract void CreateDataXMLNodesInElement(System.Xml.XmlNode data);
    }
}
