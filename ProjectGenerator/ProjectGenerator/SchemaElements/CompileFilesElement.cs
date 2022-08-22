using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ProjectGenerator.Utility;

namespace ProjectGenerator.SchemaElements
{
    class CompileFilesElement : GetFilesElement
    {
        public CompileFilesElement() : base()
        {
            SearchPatternArg = "*.cpp";
        }

        public override void AddDataToMetaInformation(GroupMetaInformation meta)
        {
            foreach (string s in m_FilePathsFromQuery)
            {
                meta.AddItem(s, null, ItemElementType.Compile);
            }
        }
    }
}