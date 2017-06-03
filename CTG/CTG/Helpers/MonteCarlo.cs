using CTG.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CTG.Helpers
{
    public static class MonteCarlo
    {
        public static string Test(int maxVertices, int rounds)
        {
            Random random = new Random();
            TimeSpan alg1Time = new TimeSpan();
            TimeSpan alg2Time = new TimeSpan();
            int alg1successes = 0;
            int alg2successes = 0;
            int alg1index = 0;
            int alg2index = 0;
            
            for (int i = 0; i < rounds; i++)
            {
                var graph = GenerateRandomGraph(maxVertices);
                Tuple<List<Point>, List<int>> coloredVertices;
                int index1, index2;

                var watch = System.Diagnostics.Stopwatch.StartNew();
                coloredVertices = Algorithms.Algorithm1.ColorGraph(graph);
                watch.Stop();
                alg1Time += watch.Elapsed;
                if (CommonHelper.isCorrectColoring(graph, coloredVertices.Item1))
                {
                    alg1successes++;
                    index1 = coloredVertices.Item2.Count;
                }
                else
                    index1 = graph.Vertices;

                watch = System.Diagnostics.Stopwatch.StartNew();
                coloredVertices = Algorithms.Algorithm2.ColorGraph(graph);
                watch.Stop();
                alg2Time += watch.Elapsed;
                if (CommonHelper.isCorrectColoring(graph, coloredVertices.Item1))
                {
                    alg2successes++;
                    index2 = coloredVertices.Item2.Count;
                }
                else
                    index2 = graph.Vertices;

                if (index1 < index2)
                    alg1index++;
                else
                {
                    if (index2 < index1)
                        alg2index++;
                }
            }

            string result = string.Format("Algorithm 1 - Less colors: {5}\nAlgorithm 2 - Less colors: {6}, trials: {2}",
                alg1Time,
                alg1successes,
                rounds,
                alg2Time,
                alg2successes,
                alg1index,
                alg2index);
            return result;
        }

        static Graph GenerateRandomGraph(int maxVertices)
        {
            Random random = new Random();
            int vertices = random.Next(maxVertices) + 1;
            int edgesCount = random.Next(vertices * (vertices - 1) / 2);
            Graph graph = new Graph(vertices);
            bool[,] edges = new bool[vertices, vertices];

            while (edgesCount-- > 0)
            {
                int from, to;
                do
                {
                    from = random.Next(vertices);
                    to = random.Next(vertices);
                } while (from == to || edges[from, to] || edges[to, from]);
                edges[from, to] = true;
            }

            for (int i = 0; i < vertices; i++)
                for (int j = 0; j < vertices; j++)
                {
                    if (edges[i, j])
                        graph.Edges[i].Add(j);
                }
            graph.Loaded = true;
            return graph;
        }
    }
}
