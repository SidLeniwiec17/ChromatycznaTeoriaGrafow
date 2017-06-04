using CTG.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CTG.Helpers
{
    public static class Algorithm2Hepler
    {
        /// <summary>
        /// Returns list of vertices with degrees: Item1 is the vertex index, Item2 is its degree.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static List<Tuple<int, int>> GetVerticesWithDegrees(Graph graph, List<int> vertices)
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            int[] degrees = new int[graph.Vertices];
            

            for (int i = 0; i < graph.Vertices; i++)
            {
                if (vertices.Contains(i))
                {
                    foreach (var v in graph.Edges[i])
                        if (vertices.Contains(v))
                        {
                            degrees[v]++;
                            degrees[i]++;
                        }
                }
                else
                    degrees[i] = -1;
            }
            for (int i = 0; i < degrees.Length; i++)
            {
                if (degrees[i]>=0)
                {
                    result.Add(new Tuple<int, int>(i, degrees[i]));
                }
            }
            return result;
        }

        public static void EraseVertices(List<int> vertices, Graph graph, int vertex, int[] colors)
        {
            vertices.Remove(vertex);

            vertices.RemoveAll(x => graph.Edges[vertex].Contains(x));
            vertices.RemoveAll(x => graph.Edges[x].Contains(vertex));

            List<int> colorsToCheck = new List<int>();
            //vertex -> color
            foreach(var e in graph.Edges[vertex])
            {
                if (colors[e] > 0)
                {
                    for (int i = 0; i < graph.Vertices; i++)
                    {
                        if (colors[i] == colors[e])
                            vertices.RemoveAll(x => graph.Edges[i].Contains(x));
                    }
                }
            }

            //color -> vertex
            for (int i = 0; i < graph.Vertices; i++)
            {
                if (graph.Edges[i].Contains(vertex) && colors[i] > 0)
                {
                    colorsToCheck.Add(colors[i]);
                }
            }
            for (int i = 0; i < graph.Vertices; i++)
            {
                if (graph.Edges[i].Any(x => colorsToCheck.Contains(colors[x])))
                {
                    vertices.Remove(i);
                }
            }

            foreach (int v in graph.Edges[vertex])
            {
                vertices.RemoveAll(x => graph.Edges[v].Contains(x));
            }

            for (int i = 0; i < graph.Vertices; i++)
            {
                if (graph.Edges[i].Contains(vertex))
                {
                    vertices.RemoveAll(x => graph.Edges[x].Contains(i));
                }
            }
        }
    }
}
