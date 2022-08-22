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
            DirectoryArg = Arguments.ProjectDirectory;
            SearchOptionArg = SearchOption.TopDirectoryOnly;
        }

        public virtual void Initialize()
        {
            foreach (string s in Directory.GetFiles(DirectoryArg, SearchPatternArg, SearchOptionArg))
            {
                m_FilePathsFromQuery.Add(s);
            }
        }

        public virtual void LoadPropertiesFromXML(System.Xml.XmlNode node)
        {
            string attributeField = XMLHelpers.GetAttributeFieldOrNull(node, "Directory") ?? "";
            DirectoryArg = FileSystemHelpers.ConvertPathToAbsolute(attributeField);

            SearchPatternArg = XMLHelpers.GetAttributeFieldOrNull(node, "SearchPattern") ?? SearchPatternArg;

            Enum.TryParse(node.Attributes.GetNamedItem("SearchOption")?.Value, out SearchOption SearchOptionArg);
        }

        public abstract void AddDataToMetaInformation(GroupMetaInformation meta);
    }
}
