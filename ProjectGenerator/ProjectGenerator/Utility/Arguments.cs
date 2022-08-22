using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectGenerator.Utility
{
    //Static instance for app arguments - globally accessible
    public static class Arguments
    {
        public static string ImportProjectFile = ""; //Required
        public static string ImportConfigFile = "";  //Optional - deduced from project file otherwise
        public static string OutputProjectFile = ""; //Optional - uses ImportProjectFile if not provided
        public static string ProjectDirectory = ""; //Optional - uses ImportProjectFile directory if not provided

        static bool HasArgumentAtIndex(string[] args, int index)
        {
            return index < args.Length;
        }

        public static bool ProcessArguments(string[] args)
        {
            //Arg 0 - Import project 
            {
                bool validArgCount = HasArgumentAtIndex(args, 0);
                Debug.Assert(validArgCount, "Not enough arguments to parse project file");
                if (!validArgCount)
                {
                    return false;
                }

                bool validPathToTemplate = System.IO.File.Exists(args[0]);
                Debug.Assert(validPathToTemplate, "Project file at path %s does not exist", args[0]);
                if (!validPathToTemplate)
                {
                    return false;
                }

                ImportProjectFile = args[0];
            }

            //Arg 1 - Import config (later on in flow if this was absent it makes a config from proj data provided)
            bool arg1Exists = HasArgumentAtIndex(args, 1);
            if (arg1Exists)
            {
                bool validPathToConfig = System.IO.File.Exists(args[1]);
                Debug.Assert(validPathToConfig, "Import config file specified but the file at path %s does not exist", args[1]);
                if (!validPathToConfig)
                {
                    return false;
                }

                ImportConfigFile = args[1];
            }

            //Arg 2 - Export location
            bool arg2Exists = HasArgumentAtIndex(args, 2);
            if (arg2Exists)
            {
                bool validPathToConfig = System.IO.File.Exists(args[2]);
                Debug.Assert(validPathToConfig, "Import config file specified but the file at path %s does not exist", args[2]);
                if (!validPathToConfig)
                {
                    return false;
                }
                //string exportSavePath = args.Length > 1 ? args[1] : System.IO.Path.ChangeExtension(args[0], ".vcxproj");                
                OutputProjectFile = args[2];
            }
            else
            {
                OutputProjectFile = ImportProjectFile;
            }

            //Arg 3 - Project directory
            bool arg3Exists = HasArgumentAtIndex(args, 3);
            if (arg3Exists)
            {
                bool directoryPathExists = System.IO.Directory.Exists(args[3]);
                Debug.Assert(directoryPathExists, "ProjectDirectory specified but the directory does not exist at path %s", args[3]);
                if (!directoryPathExists)
                {
                    return false;
                }
                ProjectDirectory = args[3];
            }
            else
            {
                ProjectDirectory = System.IO.Path.GetDirectoryName(ImportProjectFile);
            }

            //Valid args
            return true;
        }
    }
}
