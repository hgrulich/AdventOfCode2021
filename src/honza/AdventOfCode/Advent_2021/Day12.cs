using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_2021
{
    internal class Day12
    {
        public int SolveFirst()
        {
            var chartEdges = File.ReadAllLines(Path.Combine("Input", "Day12.txt"))
                .Select(x => x.Split('-'))
                .Select(x => (first: x[0], second: x[1]));

            var chart = new List<Node>();

            foreach (var chartEdge in chartEdges)
            {
                var firstNode = chart.FirstOrDefault(n => n.Name == chartEdge.first);
                if (firstNode is null)
                {
                    firstNode = new Node(chartEdge.first);
                    chart.Add(firstNode);
                }

                var secondNode = chart.FirstOrDefault(n => n.Name == chartEdge.second);
                if (secondNode is null)
                {
                    secondNode = new Node(chartEdge.second);
                    chart.Add(secondNode);
                }

                firstNode.AdjacentNodes.Add(secondNode);
                secondNode.AdjacentNodes.Add(firstNode);
            }

            var startNode = chart.First(x => x.Name == "start");
            var endNode = chart.First(x => x.Name == "end");

            var pathsToExplore = new List<CavePath>();
            foreach (var adjNode in startNode.AdjacentNodes)
            {
                pathsToExplore.Add(new CavePath(new Node[] { startNode }, adjNode));
            }
            var pathsCounter = 0;
            while (pathsToExplore.Count > 0)
            {
                var path = pathsToExplore.First();
                if (path.NodeToExplore == endNode)
                {
                    pathsCounter++;
                }
                else
                {
                    foreach (var adjNode in path.NodeToExplore.AdjacentNodes)
                    {
                        if (adjNode.IsBig || !path.VisitedNodes.Contains(adjNode))
                        {
                            var nextVisited = path.VisitedNodes.Concat(new Node[] { path.NodeToExplore }).ToArray();
                            pathsToExplore.Add(new CavePath(nextVisited, adjNode));
                        }
                    }
                }

                pathsToExplore.Remove(path);
            }
            

            return pathsCounter;
        }

        public int SolveSecond()
        {
            var chartEdges = File.ReadAllLines(Path.Combine("Input", "Day12.txt"))
                .Select(x => x.Split('-'))
                .Select(x => (first: x[0], second: x[1]));

            var chart = new List<Node>();

            foreach (var chartEdge in chartEdges)
            {
                var firstNode = chart.FirstOrDefault(n => n.Name == chartEdge.first);
                if (firstNode is null)
                {
                    firstNode = new Node(chartEdge.first);
                    chart.Add(firstNode);
                }

                var secondNode = chart.FirstOrDefault(n => n.Name == chartEdge.second);
                if (secondNode is null)
                {
                    secondNode = new Node(chartEdge.second);
                    chart.Add(secondNode);
                }

                firstNode.AdjacentNodes.Add(secondNode);
                secondNode.AdjacentNodes.Add(firstNode);
            }

            var startNode = chart.First(x => x.Name == "start");
            var endNode = chart.First(x => x.Name == "end");

            var pathsToExplore = new List<CavePath>();
            foreach (var adjNode in startNode.AdjacentNodes)
            {
                pathsToExplore.Add(new CavePath(new Node[] { startNode }, adjNode));
            }
            var pathsCounter = 0;
            var paths = new List<CavePath>();
            while (pathsToExplore.Count > 0)
            {
                var path = pathsToExplore.First();
                if (path.NodeToExplore == endNode)
                {
                    pathsCounter++;
                    paths.Add(path);
                }
                else
                {
                    foreach (var adjNode in path.NodeToExplore.AdjacentNodes)
                    {
                        if (path.ToString() == "start,A,b,A,c,A")
                        {
                        }
                        if (!adjNode.IsBig && path.VisitedNodes.Contains(adjNode) && !path.VisitedSmallTwice && adjNode != startNode)
                        {
                            var nextVisited = path.VisitedNodes.Concat(new Node[] { path.NodeToExplore }).ToArray();
                            pathsToExplore.Add(new CavePath(nextVisited, adjNode, true));
                        }
                        else if ((adjNode.IsBig || !path.VisitedNodes.Contains(adjNode)))
                        {
                            var nextVisited = path.VisitedNodes.Concat(new Node[] { path.NodeToExplore }).ToArray();
                            pathsToExplore.Add(new CavePath(nextVisited, adjNode, path.VisitedSmallTwice));
                        }
                    }
                }

                pathsToExplore.Remove(path);
            }


            return pathsCounter;
        }

        class Node
        {
            public IList<Node> AdjacentNodes { get; set; } = new List<Node>();
            public string Name { get; init; }
            public bool IsBig { get; init; }
            public Node(string name)
            {
                this.Name = name;
                this.IsBig = this.Name == this.Name.ToUpper();
            }
            public override string ToString()
            {
                return this.Name;
            }
        }

        class CavePath
        {
            public CavePath(Node[] visitedNodes, Node nodeToExplore, bool visitedTwice = false)
            {
                VisitedNodes = visitedNodes;
                NodeToExplore = nodeToExplore;
                VisitedSmallTwice = visitedTwice;
            }
            public bool VisitedSmallTwice { get; set; }
            public Node[] VisitedNodes { get; init; }
            public Node NodeToExplore { get; set; }

            public override string ToString()
            {
                var sb = new StringBuilder();
                foreach (var node in VisitedNodes)
                {
                    sb.Append(node.ToString());
                    sb.Append(",");
                }
                sb.Append(this.NodeToExplore.ToString());

                return sb.ToString();
            }

        }
    }
}
