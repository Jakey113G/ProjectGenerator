using System;

namespace ProjectGenerator
{
    //I've made this utility because the project files and filters require manual merging for adding files in. 
    //All this does is search through an existing project or filter file, it parses existing groups to know of what files are includes already.
    //Then it will add in files that are missing based on the config file.
    
    //Additionally this helps to work around https://docs.microsoft.com/en-us/cpp/build/reference/vcxproj-files-and-wildcards?view=msvc-160 so we can generate all the individual items 
    class ProjectGenerator
    {            
        static void Main(string[] args)
        {
            //Read and process argument data
            if(!Utility.Arguments.ProcessArguments(args))
            {
                return;
            }

            //Import files - inspect file system and make changes to project file
            {
                //Load project file (vcxproj, vcxproj.filters, etc)
                string importTemplatePath = Utility.Arguments.ImportProjectFile;
                ProjectDocumentData data = new ProjectDocumentData(importTemplatePath);

                //Load/Create config that will be applied to the project
                ConfigData config = new ConfigData();
                if (Utility.Arguments.ImportConfigFile.Length == 0)
                {
                    //Make default one from existing document; e.g basic functionality like adding in files in same directory as existing includes
                    config.CreateBasicConfigFromProjectXML(data);
                }
                else
                {
                    //Import config that specifies what/how to change existing project files
                    config.ImportConfig(Utility.Arguments.ImportConfigFile);
                }

                data.GenerateProjectChanges(config.GetMetaInformation());
                data.ExportParsedDocument(Utility.Arguments.OutputProjectFile);
            }
        }
    }
}
