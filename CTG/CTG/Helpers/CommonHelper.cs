using CTG.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CTG.Helpers
{
    public class CommonHelper
    {
        public static bool isCorrectColoring(Graph graph, List<Point> coloredVertices)
        {
            bool result = true;
            List<int> roots = Algorithm1Helpers.GetRoots(graph);
            for (int i = 0; i < roots.Count; i++)
            {
                List<int> colors = new List<int>();
                colors.Add(Algorithm1Helpers.GetColorFromList(coloredVertices, roots[i]));
                if (!serachPath(colors, roots[i], graph, coloredVertices))
                {
                    result = false;
                }
            }
            return result;
        }
        public static bool serachPath(List<int> currentColors, int vertice, Graph graph, List<Point> coloredVertices)
        {
            bool result = true;
            for (int i = 0; i < graph.Vertices; i++)
            {
                if (Algorithm1Helpers.IsConnection(i, vertice, graph))
                {
                    int newColor = Algorithm1Helpers.GetColorFromList(coloredVertices, i);
                    if(currentColors.Contains(newColor))
                    {
                        result = false;
                        return result;
                    }
                    else
                    {
                        List<int> newListOfColors = new List<int>(currentColors);
                        newListOfColors.Add(newColor);
                        result = serachPath(newListOfColors, i, graph, coloredVertices);
                    }
                }
            }
            return result;
        }
        public static string PrintResult(List<Point> coloredVertices, List<int> colors, Stopwatch elapsedTime)
        {
            string result = "";
            int usedColors = colors.Count();
            TimeSpan t = TimeSpan.FromMilliseconds(elapsedTime.ElapsedMilliseconds);
            string time = string.Format("{0:D2}h : {1:D2}m : {2:D2}s : {3:D2}ms",
                         t.Hours,
                         t.Minutes,
                         t.Seconds,
                         t.Milliseconds);
            string firstLine = String.Format("Algorithm1 used {0} colors in time \n {1}", usedColors, time);
            string secondLine = "Answers in format (vertice,color)";
            result = firstLine + "\n" + secondLine + "\n";
            for (int i = 0; i < coloredVertices.Count; i++)
            {
                string tmp = String.Format("({0}, {1})\n", (int)coloredVertices[i].X, (int)coloredVertices[i].Y);
                result += tmp;
            }
            return result;
        }
    }
}
