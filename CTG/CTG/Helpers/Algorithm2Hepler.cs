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
            bool[] directions = new bool[2];
            List<int> colorsToCheck = new List<int>();

            vertices.Remove(vertex);

            if (graph.Edges[vertex].Count > 0)
                directions[0] = true;
            foreach (var to in graph.Edges[vertex])
            {
                if (colors[to] > 0)
                {
                    colorsToCheck.Add(colors[to]);
                }
                vertices.Remove(to);
            }
            for (int i = 0; i < graph.Edges.Count; i++)
            {
                if (graph.Edges[i].Count > 0 && colors[i] > 0 && colorsToCheck.Contains(colors[i]))
                {
                        foreach (int v in graph.Edges[i])
                    vertices.Remove(v);
                }
            }
            colorsToCheck.RemoveAll(x => true);


            for (int i = 0; i < graph.Edges.Count; i++)
            {
                if (i == vertex)
                    continue;
                if (graph.Edges[i].Contains(vertex))
                {
                    directions[1] = true;
                    vertices.Remove(i);
                    if (colors[i] > 0)
                        colorsToCheck.Add(colors[i]);
                }
            }
            for (int i = 0; i < graph.Edges.Count; i++)
            {
                bool erase = false;
                foreach(var v in graph.Edges[i])
                {
                    if (colorsToCheck.Contains(colors[v]))
                        erase = true;
                }
                if (erase)
                    vertices.Remove(i);
            }
        }
    }
}
