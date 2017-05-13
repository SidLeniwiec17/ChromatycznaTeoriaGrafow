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
    public class Algorithm1Helpers
    {
        public static List<int> GetSortedVertices(Graph graph)
        {
            List<int> answerVertices = new List<int>();
            List<Point> vertices = GetVerticesWithInNumbersFromGraph(graph);
            QuickSortVertices(vertices, 0, vertices.Count - 1);
            foreach (Point v in vertices)
            {
                answerVertices.Add((int)v.X);
            }
            return answerVertices;
        }
        public static void QuickSortVertices(List<Point> vertices, int left, int right)
        {
            //quicksort
            var i = left;
            var j = right;
            var pivot = vertices[(left + right) / 2].Y;
            while (i < j)
            {
                while (vertices[i].Y < pivot) i++;
                while (vertices[j].Y > pivot) j--;
                if (i <= j)
                {
                    // swap
                    var tmp = vertices[i];
                    vertices[i++] = vertices[j];  // ++ and -- inside array braces for shorter code
                    vertices[j--] = tmp;
                }
            }
            if (left < j) QuickSortVertices(vertices, left, j);
            if (i < right) QuickSortVertices(vertices, i, right);
        }
        public static List<Point> GetVerticesWithInNumbersFromGraph(Graph graph)
        {
            List<Point> vertices = new List<Point>();
            for (int i = 0; i < graph.Vertices; i++)
            {
                int n = 0;
                for (int j = 0; j < graph.Edges.Count; j++)
                {
                    for (int p = 0; p < graph.Edges[j].Count; p++)
                    {
                        if (graph.Edges[j][p] == i)
                        {
                            n++;
                        }
                    }
                }
                vertices.Add(new Point(i, n));
            }
            return vertices;
        }
        public static int AddColorToList(List<int> colors)
        {
            int newColor = 0;
            if (colors.Count < 1)
            {
                colors.Add(0);
            }
            else
            {
                int lastColor = colors[colors.Count - 1];
                newColor = lastColor + 1;
                colors.Add(newColor);
            }
            return newColor;
        }
        public static int TryGetColorOfVertice(int vertice, List<int> colors, List<Point> coloredVertices, Graph graph)
        {
            int color = -1;
            //j
            int currentIndex = -1;
            int currentColor = -1;
            for (int i = 0; i < coloredVertices.Count(); i++)
            {
                if (IsRoot(vertice, graph))
                {
                    color = 0;
                }
                else if (IsConnection(vertice, (int)coloredVertices[i].X, graph))
                {
                    int tempColor = GetColorFromList(coloredVertices, (int)coloredVertices[i].X);
                    if (tempColor >= currentColor)
                    {
                        currentIndex = i;
                        currentColor = tempColor;
                    }
                }
            }
            if (currentColor >= 0 && currentIndex >= 0)
            {
                if (colors[colors.Count - 1] == currentColor)
                {
                    color = Algorithm1Helpers.AddColorToList(colors);
                }
                else
                {
                    color = currentColor + 1;
                }
            }
            return color;
        }
        public static int GetColorFromList(List<Point> colors, int vertice)
        {
            int color = -1;
            foreach (var p in colors)
            {
                if (p.X == vertice)
                {
                    color = (int)p.Y;
                }
            }
            return color;
        }
        public static bool IsConnection(int to, int from, Graph graph)
        {
            bool isConnected = false;
            for (int i = 0; i < graph.Edges[from].Count; i++)
            {
                if (graph.Edges[from][i] == to)
                {
                    isConnected = true;
                    break;
                }
            }
            return isConnected;
        }
        public static List<int> GetRoots(Graph graph)
        {
            List<int> roots = new List<int>();
            for (int i = 0; i < graph.Vertices; i++)
            {
                if (IsRoot(i, graph))
                {
                    roots.Add(i);
                }
            }
            return roots;
        }
        public static bool IsRoot(int vertice, Graph graph)
        {
            bool root = true;
            for (int i = 0; i < graph.Vertices; i++)
            {
                if (i != vertice && IsConnection(vertice, i, graph))
                {
                    root = false;
                    break;
                }
            }
            return root;
        }
        public static int IsPossibleToColor(int color, int vertice, List<int> colors, List<Point> coloredVertices, Graph graph)
        {
            int newColor = color;
            int maxColor = -1;
            bool haveToChangeColor = false;
            for (int i = 0; i < coloredVertices.Count; i++)
            {
                if (IsConnection((int)coloredVertices[i].X, vertice, graph))
                {
                    maxColor = (int)coloredVertices[i].Y;
                    if ((int)coloredVertices[i].Y == color)
                    {
                        haveToChangeColor = true;
                    }
                }
            }
            if (haveToChangeColor)
            {
                if (maxColor == colors[colors.Count - 1])
                {
                    newColor = AddColorToList(colors);
                }
                else
                {
                    newColor = maxColor + 1;
                }
            }
            return newColor;
        }
    }
}
