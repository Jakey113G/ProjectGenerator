using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectGenerator.SchemaElements
{
    //Config files use standard msbuild project schema nodes and some custom nodes
    interface ISchemaElement
    {
        public abstract void LoadPropertiesFromXML(System.Xml.XmlNode s);
        public abstract void Initialize();

        public abstract void AddDataToMetaInformation(GroupMetaInformation meta); 
    }
}
