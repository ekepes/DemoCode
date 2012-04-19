using System.Collections.Generic;

namespace MSBuildPathLister
{
    public class Target
    {
        public Target(string name, 
                      string dependencyList, 
                      string projectFilename)
        {
            Name = name;
            DependencyList = dependencyList;
            ProjectFilename = projectFilename;
            RemoteDependencyLists = new List<RemoteDependencyList>();
        }

        public string DependencyList { get; private set; }

        public void AddItemToDependencyList(string target)
        {
            if (DependencyList.Length > 0)
            {
                DependencyList += ";";
            }
            DependencyList += target;
        }

        public List<RemoteDependencyList> RemoteDependencyLists { get; private set; }

        public string Name { get; private set; }

        public string ProjectFilename { get; private set; }

        public List<Target> Dependencies { get; private set; }

        public void SetDependencies(List<Target> targets)
        {
            Dependencies = targets;
        }

        public bool AreDependenciesResolved()
        {
            return (Dependencies != null);
        }

        public void AddRemoteDependencyList(string projectFile, string dependencyList)
        {
            RemoteDependencyLists.Add(new RemoteDependencyList(projectFile, dependencyList));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}