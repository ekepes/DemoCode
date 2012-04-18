using System.Collections.Generic;
using System.IO;
using System.Reflection;

using log4net;

namespace MSBuildPathLister
{
    public class ParsedProject
    {
        private static readonly ILog _Log =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _baseDirectory;

        private readonly string _masterProjectFilename;

        public ParsedProject(string masterProjectFilename)
        {
            _baseDirectory = Path.GetDirectoryName(masterProjectFilename);
            _masterProjectFilename = Path.GetFileName(masterProjectFilename);

            ProjectFiles = new List<string>();
            BuildProperties = new Dictionary<string, string>
                {
                    { "MSBuildProjectDirectory", _baseDirectory }
                };
            Targets = new Dictionary<string, Target>();
        }

        public Dictionary<string, string> BuildProperties { get; private set; }

        public List<string> ProjectFiles { get; private set; }

        public Dictionary<string, Target> Targets { get; private set; }

        public bool HasProjectBeenParsed(string projectFilename)
        {
            return ProjectFiles.Contains(GetRelativeFilename(projectFilename));
        }

        public void AddProjectFile(string projectFilename)
        {
            ProjectFiles.Add(GetRelativeFilename(projectFilename));
        }

        public Target AddTarget(string name, string dependencies, string parentProjectFilename)
        {
            string actualName = name;
            string relativeFilename = GetExpandedRelativeFilename(parentProjectFilename);
            string namespacedName = BuildNamespacedName(name, relativeFilename);

            while (Targets.ContainsKey(namespacedName))
            {
                namespacedName += "X";
            }

            Target target = new Target(actualName, dependencies, relativeFilename);
            Targets.Add(namespacedName, target);

            return target;
        }

        private string BuildNamespacedName(string name, string relativeFilename)
        {
            return string.Format("{0}:{1}", relativeFilename, name);
        }

        public void AddBuildProperty(string name, string value)
        {
            while (BuildProperties.ContainsKey(name))
            {
                name += "X";
            }
            
            BuildProperties.Add(name, value);
        }

        private string GetRelativeFilename(string projectFilename)
        {
            string relativeFilename;

            if (projectFilename.Contains(_baseDirectory))
            {
                relativeFilename = projectFilename.Substring(_baseDirectory.Length + 1);
            }
            else
            {
                relativeFilename = projectFilename;
            }
            return relativeFilename;
        }

        public string GetExpandedRelativeFilename(string filename)
        {
            return GetRelativeFilename(ExpandTokens(filename));
        }

        public string ExpandTokens(string value)
        {
            string result = value;
            while (result.Contains("$("))
            {
                int tokenStart = result.IndexOf("$(") + 2;
                int tokenEnd = result.IndexOf(")", tokenStart);
                string token = result.Substring(tokenStart, tokenEnd - tokenStart);
                if (BuildProperties.ContainsKey(token))
                {
                    result = result.Replace(string.Format("$({0})", token), BuildProperties[token]);
                }
            }

            return result;
        }

        public void BuildTree()
        {
            foreach (KeyValuePair<string, Target> keyValuePair in Targets)
            {
                Target target = keyValuePair.Value;
                ResolveDependencies(target, 0);
            }
        }

        private void ResolveDependencies(Target target, int level)
        {
            if (!target.AreDependenciesResolved())
            {
                _Log.DebugFormat("Resolving dependencies for Target {0} at level {1}", target.Name, level);

                List<Target> targets = new List<Target>();

                ParseDependencyList(level, target.DependencyList, target.ProjectFilename, targets);
                foreach (RemoteDependencyList item in target.RemoteDependencyLists)
                {
                    ParseDependencyList(level, item.DependencyList, item.ProjectFile, targets);
                }

                target.SetDependencies(targets);
            }
        }

        private void ParseDependencyList(int level, string dependencyList, string projectFilename, List<Target> targets)
        {
            string[] dependencies = dependencyList.Split(';');
            foreach (string dependency in dependencies)
            {
                string namespacedName = BuildNamespacedName(dependency, projectFilename);
                if (Targets.ContainsKey(namespacedName))
                {
                    Target child = Targets[namespacedName];
                    ResolveDependencies(child, level + 1);
                    targets.Add(child);
                }
            }
        }

        public void WriteTreeToFile(string filename)
        {
            WriteTreeToFile(filename, string.Empty);
        }

        public void WriteTreeToFile(string filename, string targetToWrite)
        {
            using (StreamWriter writer = File.CreateText(filename))
            {
                foreach (Target target in Targets.Values)
                {
                    if (target.ProjectFilename == _masterProjectFilename)
                    {
                        if (!string.IsNullOrWhiteSpace(targetToWrite) && targetToWrite != target.Name)
                        {
                            continue;
                        }
                        WriteToFileAtLevel(writer, target, 0);
                    }
                }
            }
        }

        private void WriteToFileAtLevel(StreamWriter writer, Target target, int level)
        {
            writer.WriteLine("{0}{1}:{2}",
                    "".PadLeft(level, '\t'),
                    target.ProjectFilename,
                    target.Name);
            foreach (Target child in target.Dependencies)
            {
                WriteToFileAtLevel(writer, child, level + 1);
            }
        }
    }
}