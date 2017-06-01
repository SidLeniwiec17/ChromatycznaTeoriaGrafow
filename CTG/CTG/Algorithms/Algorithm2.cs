using CTG.Content;
using CTG.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CTG.Algorithms
{
    public class Algorithm2
    {
        public static Tuple<List<Point>, List<int>> ColorGraph(Graph graph)
        {
            //NC
            var notColoredVertices = Enumerable.Range(0, graph.Vertices).ToList();
            //HT
            var helperList = notColoredVertices.ToList();
            //LC
            int[] colors = new int[graph.Vertices];

            int i = 1;

            while(notColoredVertices.Count > 0)
            {
                helperList = notColoredVertices.ToList();
                while(helperList.Count > 0)
                {
                    int vertex = Algorithm2Hepler.GetVerticesWithDegrees(graph, helperList).First().Item1;
                    colors[vertex] = i;
                    Algorithm2Hepler.EraseVertices(helperList, graph, vertex, colors);
                }
                notColoredVertices.RemoveAll(x => colors[x] > 0);
                i++;
            }
            var coloredVertices = new List<Point>(graph.Vertices);
            var colorsList = new List<int>();
            for (int j = 0; j < colors.Length; j++)
            {
                coloredVertices.Add(new Point(j, colors[j]));
                if (!colorsList.Contains(colors[j]))
                    colorsList.Add(colors[j]);
            }

            return new Tuple<List<Point>, List<int>>(coloredVertices, colorsList);
        }
    }
}
