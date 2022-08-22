using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectGenerator
{
    public enum ItemElementType
    {
        Compile = 0,
        Include = 1
    }

    public struct ItemData
    {
        public string AbsolutePath;
        public string FilterPath; //Null if don't add filter (not nessarily a file system path)
        public ItemElementType ItemType;
    }

    public class GroupMetaInformation
    {
        public List<ItemData> m_ElementData = new List<ItemData>();

        public void AddItem(string path, string filterPath, ItemElementType type)
        {
            m_ElementData.Add(new ItemData { AbsolutePath=path, FilterPath=filterPath, ItemType=type });
        }
        public void AddItem(ItemData data)
        {
            m_ElementData.Add(data);
        }
    }

    //When we load a config file we process that information into data to tells us what to add/remove from the data inside a project.
    //This class represents that information
    public class ProjectMetaInformation
    {
        private List<GroupMetaInformation> m_GroupMetaInformation = new List<GroupMetaInformation>();

        public void AddGroupMeta(GroupMetaInformation meta)
        {
            m_GroupMetaInformation.Add(meta);
        }

        public List<GroupMetaInformation> GetGroupMetaInformation()
        {
            return m_GroupMetaInformation;
        }
    }
}
