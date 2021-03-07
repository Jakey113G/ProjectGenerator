using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectGenerator.SchemaElements
{
    interface ISchemaElement
    {
        public abstract void LoadPropertiesFromXML(System.Xml.XmlNode s);
        public abstract void CreateDataXMLNodesInElement(System.Xml.XmlNode data);
    }
}
