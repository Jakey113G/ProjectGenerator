using System;
using System.Diagnostics;

namespace ProjectGenerator
{
    //I've made this utility because the project files we use are too complex to resolve manually and apparently changing it is not possible. 
    //All this does is search through a template xml file (mostly the project file) and parses specialized xml nodes to substitute some data in to.
    //additionally it works around https://docs.microsoft.com/en-us/cpp/build/reference/vcxproj-files-and-wildcards?view=msvc-160 so we can generate all the individual items 
    class ProjectGenerator
    {            
        static void Main(string[] args)
        {
            //Argument checks
            {
                bool validArgCount = args.Length >= 1;
                Debug.Assert(validArgCount, "Not enough arguments to parse template into project file");
                if (!validArgCount)
                { 
                    return; 
                }

                bool validPathToTemplate = System.IO.File.Exists(args[0]);
                Debug.Assert(validPathToTemplate, "Template file at path %s does not exist", args[0]);
                if(!validPathToTemplate)
                {
                    return;
                }

                bool validExtensionOnTemplate = System.IO.Path.GetExtension(args[0]) == ".xml";
                Debug.Assert(validExtensionOnTemplate, "Template file of type %s when we expected .xml", System.IO.Path.GetExtension(args[0]));
                if (!validExtensionOnTemplate)
                {
                    return;
                }
            }

            //main program
            {
                string importTemplatePath = args[0];
                ProjectDocumentData data = new ProjectDocumentData(importTemplatePath);

                //Use arg or form a new path from template file
                string exportSavePath = args.Length > 1 ? args[1] : System.IO.Path.ChangeExtension(args[0], ".vcxproj");                
                data.ExportParsedDocument(exportSavePath);
            }
        }
    }
}
