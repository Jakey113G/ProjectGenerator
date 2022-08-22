using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ProjectGenerator.SchemaElements;
using System.Collections.ObjectModel;
using ProjectGenerator.Utility;

namespace ProjectGenerator
{
    //Representation of the document
    class ProjectDocumentData
    {
        private XmlDocument doc;

        public ProjectDocumentData(string path)
        {
            doc = new XmlDocument();
            doc.Load(path);
        }

        public void GenerateProjectChanges(ProjectMetaInformation metaInformation)
        {
            //Specified by https://docs.microsoft.com/en-us/cpp/build/walkthrough-using-msbuild-to-create-a-visual-cpp-project?view=msvc-170
            var groupList = doc.GetElementsByTagName("ItemGroup");

            int index = 0;
            foreach(var groupMeta in metaInformation.GetGroupMetaInformation())
            {
                var documentGroup = groupList.Item(index);

                //Trust our meta data instead of what is already here (in future maybe we do merging)
                documentGroup.RemoveAll();

                foreach(var fileData in groupMeta.m_ElementData)
                {
                    switch(fileData.ItemType)
                    {
                        case ItemElementType.Compile:
                        {
                            string pathInXML = Utility.FileSystemHelpers.GetPathIfRelative(fileData.AbsolutePath, Arguments.ProjectDirectory);
                            Utility.XMLHelpers.CreateCompileNodesInGroup(documentGroup, pathInXML);
                            break;
                        }
                        case ItemElementType.Include:
                        {
                            string pathInXML = Utility.FileSystemHelpers.GetPathIfRelative(fileData.AbsolutePath, Arguments.ProjectDirectory);
                            Utility.XMLHelpers.CreateIncludeNodeInGroup(documentGroup, pathInXML, fileData.FilterPath);
                            break;
                        }
                    }
                }
                ++index;
            }
        }

        public void ExportParsedDocument(string s)
        {
            doc.Save(s);
        }

        public XmlDocument GetDocument()
        {
            return doc;
        }
    }
}
