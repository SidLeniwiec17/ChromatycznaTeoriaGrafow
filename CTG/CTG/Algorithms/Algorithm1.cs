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
    public class Algorithm1
    {
        public static Tuple<List<Point>, List<int>> ColorGraph(Graph graph)
        {
            //A
            List<Point> coloredVertices = new List<Point>();
            //SW
            List<int> sortedVertices = Algorithm1Helpers.GetSortedVertices(graph);
            //LC
            List<int> colors = new List<int>();


            Algorithm1Helpers.AddColorToList(colors);
            coloredVertices.Add(new Point(sortedVertices[0], colors[0]));
            sortedVertices.RemoveAt(0);

            while (sortedVertices.Count > 0)
            {
                for (int i = 0; i < sortedVertices.Count; i++)
                {
                    int vertice = sortedVertices[i];
                    int newColor = Algorithm1Helpers.TryGetColorOfVertice(vertice, colors, coloredVertices, graph);
                    newColor = Algorithm1Helpers.IsPossibleToColor(newColor, vertice, colors, coloredVertices, graph);
                    if (newColor >= 0)
                    {
                        coloredVertices.Add(new Point(vertice, newColor));
                        sortedVertices.RemoveAt(i);
                        break;
                    }
                }
            }
            return new Tuple<List<Point>, List<int>>(coloredVertices, colors);
        }
    }
}
