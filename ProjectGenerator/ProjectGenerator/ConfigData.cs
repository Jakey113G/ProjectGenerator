using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ProjectGenerator.SchemaElements;
using System.Collections.ObjectModel;

namespace ProjectGenerator
{
    //Process a custom xml file to dictate configuration behaviour to apply to the project
    class ConfigData
    {
        private XmlDocument configDoc;
        private static readonly Type[] CustomElements = { typeof(IncludeFilesElement), typeof(CompileFilesElement) };

        ProjectMetaInformation m_MetaInformation;

        public ConfigData()
        {

        }

        public void ImportConfig(string path)
        {
            configDoc = new XmlDocument();
            configDoc.Load(path);

            ProcessConfig();
        }

        public void ProcessConfig()
        {
            m_MetaInformation = new ProjectMetaInformation();

            XmlNodeList projectGroups = configDoc.GetElementsByTagName("ItemGroup");
            foreach (XmlElement projectGroupNode in projectGroups)
            {
                GroupMetaInformation groupMetaInfo = new GroupMetaInformation();

                //Handle elements we have handling for listed in config
                foreach (Type t in CustomElements)
                {
                    XmlNodeList list = projectGroupNode.GetElementsByTagName(t.Name);
                    if (list.Count > 0)
                    {

                        //process the custom nodes in the group that will write into meta information on the project
                        foreach (XmlNode originalNode in list)
                        {
                            ISchemaElement element = Activator.CreateInstance(t) as ISchemaElement;
                            element.LoadPropertiesFromXML(originalNode);
                            element.Initialize();
                            element.AddDataToMetaInformation(groupMetaInfo);
                        }

                    }
                }
                m_MetaInformation.AddGroupMeta(groupMetaInfo);
            }
        }

        public void CreateBasicConfigFromProjectXML(ProjectDocumentData data)
        {
            m_MetaInformation = new ProjectMetaInformation();

            XmlNodeList projectGroups = data.GetDocument().GetElementsByTagName("ItemGroup");
            foreach (XmlElement projectGroupNode in projectGroups)
            {
                XmlNodeList compile = projectGroupNode.GetElementsByTagName("ClCompile");
                XmlNodeList includes = projectGroupNode.GetElementsByTagName("ClInclude");

                GroupMetaInformation groupMetaInfo = new GroupMetaInformation();
                {
                    //Look over all CICompile for the group, create object to retrieve all the compile files based on directory top level
                    {
                        HashSet<string> uniqueDirectoryLookups = new HashSet<string>();
                        foreach (XmlNode node in compile)
                        {
                            string val = Utility.XMLHelpers.GetAttributeFieldOrNull(node, "Include");
                            if (val != null)
                            {
                                string dir = System.IO.Path.GetDirectoryName(Utility.FileSystemHelpers.ConvertPathToAbsolute(val));
                                string extensionFileQuery = "*" + System.IO.Path.GetExtension(val);
                                string uniqueQueryResult = dir + "\\" + extensionFileQuery;
                                if (!uniqueDirectoryLookups.Contains(uniqueQueryResult))
                                {
                                    CompileFilesElement comp = new CompileFilesElement();
                                    comp.DirectoryArg = dir;
                                    comp.SearchPatternArg = extensionFileQuery;
                                    comp.SearchOptionArg = System.IO.SearchOption.TopDirectoryOnly;
                                    comp.Initialize();
                                    comp.AddDataToMetaInformation(groupMetaInfo);
                                    uniqueDirectoryLookups.Add(uniqueQueryResult);
                                }
                            }
                        }
                    }

                    //Look over all CIInclude for the group, create object to retrieve all the compile files based on directory top level
                    {
                        HashSet<string> uniqueDirectoryLookups = new HashSet<string>();
                        foreach (XmlNode node in includes)
                        {
                            string val = Utility.XMLHelpers.GetAttributeFieldOrNull(node, "Include");
                            if (val != null)
                            {
                                string dir = System.IO.Path.GetDirectoryName(Utility.FileSystemHelpers.ConvertPathToAbsolute(val));
                                string extensionFileQuery = "*" + System.IO.Path.GetExtension(val);
                                string uniqueQueryResult = dir + "\\" + extensionFileQuery;
                                if (!uniqueDirectoryLookups.Contains(uniqueQueryResult))
                                {
                                    IncludeFilesElement incl = new IncludeFilesElement();
                                    incl.DirectoryArg = dir;
                                    incl.SearchPatternArg = extensionFileQuery;
                                    incl.FilterString = Utility.XMLHelpers.GetFilterPath(node as XmlElement);
                                    incl.SearchOptionArg = System.IO.SearchOption.TopDirectoryOnly;
                                    incl.Initialize();
                                    incl.AddDataToMetaInformation(groupMetaInfo);
                                    uniqueDirectoryLookups.Add(uniqueQueryResult);
                                }
                            }
                        }
                    }
                }
                m_MetaInformation.AddGroupMeta(groupMetaInfo);
            }
        }

        public ProjectMetaInformation GetMetaInformation()
        {
            return m_MetaInformation;
        }
    }
}
