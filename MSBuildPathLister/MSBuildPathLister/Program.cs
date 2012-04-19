using System;
using System.IO;

namespace MSBuildPathLister
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string filename = GetArgument(args, 0, Path.Combine(Environment.CurrentDirectory, "build.proj"));
            string baseDirectoryName = Path.GetDirectoryName(filename);
            if (baseDirectoryName.Length == 0)
            {
                filename = Path.Combine(Environment.CurrentDirectory, filename);
            }
            else
            {
                Environment.CurrentDirectory = baseDirectoryName;
            }
            string outputFolder = GetArgument(args, 1, baseDirectoryName);

            ProjectParser parser = new ProjectParser();
            ParsedProject project = new ParsedProject(filename);
            parser.ScanProject(filename, project);

            project.BuildTree();

            project.WriteReport(outputFolder);
        }

        private static string GetArgument(string[] args, int argumentPosition, string defaultValue)
        {
            if (args.Length < argumentPosition + 1)
            {
                return defaultValue;
            }
            return args[argumentPosition];
        }
    }
}