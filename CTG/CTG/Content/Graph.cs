using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTG.Content
{
    public class Graph
    {
        public int Vertices { get; set; }
        public List<List<int>> Edges { get; set; }
        public Graph()
        {

        }
        public Graph(int vertices)
        {
           Vertices = vertices;
           Edges = new List<List<int>>();
           for(int i = 0 ; i < vertices ; i++)
           {
               List<int> tmpList = new List<int>();
               Edges.Add(tmpList);
           }
        }
        public void InitializeGraph(int vertices)
        {
            Vertices = vertices;
            Edges = new List<List<int>>();
            for (int i = 0; i < vertices; i++)
            {
                List<int> tmpList = new List<int>();
                Edges.Add(tmpList);
            }
        }
    }
}
