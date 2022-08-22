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
        public string FilterString { get; set; }

        public IncludeFilesElement() : base()
        {
            SearchPatternArg = "*.h";
            FilterString = null;
        }

        public override void LoadPropertiesFromXML(System.Xml.XmlNode node)
        {
            base.LoadPropertiesFromXML(node);
            FilterString = XMLHelpers.GetAttributeFieldOrNull(node, "FilterString") ?? null;
        }

        public override void AddDataToMetaInformation(GroupMetaInformation meta)
        {
            foreach (string s in m_FilePathsFromQuery)
            {
                meta.AddItem(s, FilterString, ItemElementType.Include);
            }
        }
    }
}
