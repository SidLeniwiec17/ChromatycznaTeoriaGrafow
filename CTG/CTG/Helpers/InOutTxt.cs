using CTG.Content;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CTG.Helpers
{
    /// <summary>
    /// File pattern:
    /// number_of_vertices
    /// vertice_index: connection_to_x, connection_to_x2
    /// vertice_index: connection_to_x3
    /// vertice_index: connection_to_x4, connection_to_x1
    /// [...]
    /// example:
    /// 5
    /// 0:1
    /// 1:2,3
    /// 2:
    /// 3:4
    /// 4:
    /// </summary>
    public class InOutTxt
    {
        public static Graph LoadGraphFromFile()
        {
            Graph graph = new Graph();
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open graph instance.";
            open.Filter = "Txt File (*.txt)|*.txt";
            if (open.ShowDialog() == true)
            {
                try
                {
                    List<string> lines = new List<string>();
                    System.IO.StreamReader file =
                        new System.IO.StreamReader(open.FileName);

                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                    file.Close();
                    if (!ParseText(lines, graph))
                    {
                        MessageBox.Show("File content has wrong format !");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error ! " + ex.Message);
                }
            }
            graph.Loaded = true;
            return graph;
        }

        public static bool ParseText(List<string> text, Graph graph)
        {
            bool correct = false;
            int vertices = 0;
            if (!int.TryParse(text[0], out vertices))
                return false;
            if (vertices != text.Count - 1)
                return false;

            graph.InitializeGraph(vertices);
            try
            {
                for (int i = 1; i < text.Count; i++)
                {
                    string singleLine = text[i].Substring(text[i].IndexOf(":") + 1);
                    singleLine.Replace(" ", "");
                    string[] values = singleLine.Split(',');
                    if (values.Length > 0)
                    {
                        for (int q = 0; q < values.Length; q++)
                        {
                            if (values[0].Length > 0)
                            {
                                int singleEdge = 0;
                                if (!int.TryParse(values[q], out singleEdge))
                                    return false;
                                else
                                    graph.Edges[i - 1].Add(singleEdge);
                            }
                        }
                    }
                }
                correct = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ! " + ex.Message);
            }
            return correct;
        }
    }
}
