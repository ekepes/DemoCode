namespace MSBuildPathLister
{
    public class RemoteDependencyList
    {
        public RemoteDependencyList(string projectFile, string dependencyList)
        {
            ProjectFile = projectFile;
            DependencyList = dependencyList;
        }

        public string ProjectFile { get; private set; }

        public string DependencyList { get; private set; }
    }
}