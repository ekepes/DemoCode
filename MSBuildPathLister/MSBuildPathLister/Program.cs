using System;
using System.IO;

namespace MSBuildPathLister
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string target = GetArgument(args, 0, "CCNightly");
            string filename = GetArgument(args, 1, Path.Combine(Environment.CurrentDirectory, "build.proj"));
            string treeFilename = GetArgument(args, 2, "tree.txt");

            ProjectParser parser = new ProjectParser();
            ParsedProject project = new ParsedProject(filename);
            parser.ScanProject(filename, project);

            project.BuildTree();

            project.WriteTreeToFile(treeFilename, target);
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