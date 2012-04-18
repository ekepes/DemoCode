using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace MSBuildPathLister
{
    public class ProjectParser
    {
        private void ProcessImport(XmlNode node,
                                          string parentProjectFilename,
                                          ParsedProject project)
        {
            string importFilename = node.Attributes["Project"].InnerText;
            ScanProject(importFilename, project);
        }

        private void ProcessPropertyGroup(XmlNode node,
                                                 string parentProjectFilename,
                                                 ParsedProject project)
        {
            foreach (XmlNode property in node.ChildNodes)
            {
                if (property.NodeType == XmlNodeType.Element)
                {
                    project.AddBuildProperty(property.LocalName, property.InnerText);
                }
            }
        }

        private void ProcessTargetNode(XmlNode node,
                                              string parentProjectFilename,
                                              ParsedProject project)
        {
            string name = node.Attributes["Name"].InnerText;
            XmlNode dependsOnTargets = node.Attributes.GetNamedItem("DependsOnTargets");
            string dependencies = string.Empty;
            if (dependsOnTargets != null)
            {
                dependencies = dependsOnTargets.InnerText;
            }

            Target target = project.AddTarget(name, dependencies, parentProjectFilename);

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element && child.LocalName == "MSBuild")
                {
                    XmlNode childProjects = child.Attributes.GetNamedItem("Projects");
                    if (childProjects != null)
                    {
                        ScanProject(childProjects.InnerText, project);

                        XmlNode targets = child.Attributes.GetNamedItem("Targets");
                        if (targets != null)
                        {
                            string projectFile = project.GetExpandedRelativeFilename(childProjects.InnerText);
                            string dependencyList = targets.InnerText;
                            target.AddRemoteDependencyList(projectFile, dependencyList);
                        }
                    }
                }
            }
        }

        private delegate void NodeProcessing(XmlNode node, string parentProjectFilename, ParsedProject project);

        public void ScanProject(string filename, ParsedProject project)
        {
            Dictionary<string, NodeProcessing> processingActions = 
                new Dictionary<string, NodeProcessing>
            {
                { "PropertyGroup", ProcessPropertyGroup },
                { "Import", ProcessImport },
                { "Target", ProcessTargetNode }
            };

            string expandedFilename = project.ExpandTokens(filename);

            if (project.HasProjectBeenParsed(expandedFilename))
            {
                return;
            }

            project.AddProjectFile(expandedFilename);

            string extension = Path.GetExtension(expandedFilename).ToUpper().Trim();
            if (extension == ".SLN")
            {
                return;
            }

            XmlDocument projectFile = new XmlDocument();
            projectFile.Load(expandedFilename);

            foreach (XmlNode node in projectFile.DocumentElement.ChildNodes)
            {
                string localName = node.LocalName;
                if (processingActions.ContainsKey(localName))
                {
                    processingActions[localName](node, filename, project);
                }
            }
        }
    }
}