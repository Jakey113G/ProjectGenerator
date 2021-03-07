using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ProjectGenerator.SchemaElements;
using System.Collections.ObjectModel;

namespace ProjectGenerator
{
    //Using https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-project-file-schema-reference?view=vs-2019
    //https://docs.microsoft.com/en-us/cpp/build/walkthrough-using-msbuild-to-create-a-visual-cpp-project?view=msvc-160
    //We organise the data in this object, the Builder can then process this information into an xml
    class ProjectDocumentData
    {
        private XmlDocument doc;
        private static readonly Type[] CustomElements = { typeof(IncludeFilesElement), typeof(CompileFilesElement) };

        public ProjectDocumentData(string path)
        {
            doc = new XmlDocument();
            doc.Load(path);

            foreach (Type t in CustomElements)
            {
                XmlNodeList list = doc.GetElementsByTagName(t.Name);

                //Create & add new nodes
                foreach(XmlNode originalNode in list)
                {
                    ISchemaElement element = Activator.CreateInstance(t) as ISchemaElement;
                    element.LoadPropertiesFromXML(originalNode);
                    element.CreateDataXMLNodesInElement(originalNode.ParentNode);
                }

                //remove old nodes
                for(int i = 0; i < list.Count; ++i)
                {
                    XmlNode parent = list[i].ParentNode;
                    parent.RemoveChild(list[i]);
                }

            }
        }

        public void ExportParsedDocument(string s)
        {
            doc.Save(s);
        }
    }
}
