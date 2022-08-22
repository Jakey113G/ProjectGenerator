using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectGenerator.Utility
{
    static class FileSystemHelpers
    {
        public static string ConvertPathToAbsolute(string attributeDirectory)
        {
            if (System.IO.Path.IsPathRooted(attributeDirectory))
            {
                return attributeDirectory; //Use absolute path here
            }
            return System.IO.Path.GetFullPath( System.IO.Path.Combine(Arguments.ProjectDirectory, attributeDirectory));
        }

        public static string GetPathIfRelative(string filePath, string projectPath)
        {
            System.Diagnostics.Debug.Assert(System.IO.Path.IsPathRooted(filePath), "FilePath should be absolute");

            return filePath.StartsWith(projectPath) ? filePath.Substring(projectPath.Length + 1) : filePath;
        }

    }
}
