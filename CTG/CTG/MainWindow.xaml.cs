using CTG.Content;
using CTG.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CTG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string alg1result = "";
        public string alg2result = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoadGraph_Click(object sender, RoutedEventArgs e)
        {
            Graph graph = InOutTxt.LoadGraphFromFile();
            Console.WriteLine("Graph loaded !");
            if (graph.Loaded)
            {
                BlakWait.Visibility = Visibility.Visible;
                
                await CalculateFirstAlg(graph);
                Alg1ReultTxtBlock.Text = alg1result;
                await CalculateSecondAlg(graph);

                Alg2ReultTxtBlock.Text = alg1result;

                SummaryTextBlock.Text = MonteCarlo.Test(50, 100);
                BlakWait.Visibility = Visibility.Collapsed;

            }
        }
        public async Task CalculateFirstAlg(Graph graph)
        {
            await Task.Run(() =>
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Tuple<List<Point>, List<int>> coloredVertices = Algorithms.Algorithm1.ColorGraph(graph);
                watch.Stop();
                if (CommonHelper.isCorrectColoring(graph, coloredVertices.Item1))
                {
                    alg1result = CommonHelper.PrintResult(coloredVertices.Item1, coloredVertices.Item2, watch);
                    Console.WriteLine("Colored !");
                }
                else
                {
                    alg1result = "Wrong colloring !";
                    Console.WriteLine("Colored but wrong!");
                }
            });
        }
        public async Task CalculateSecondAlg(Graph graph)
        {
            await Task.Run(() =>
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Tuple<List<Point>, List<int>> coloredVertices = Algorithms.Algorithm2.ColorGraph(graph);
                watch.Stop();
                if (CommonHelper.isCorrectColoring(graph, coloredVertices.Item1))
                {
                    alg2result = CommonHelper.PrintResult(coloredVertices.Item1, coloredVertices.Item2, watch);
                    Console.WriteLine("Colored !");
                }
                else
                {
                    alg2result = "Wrong colloring !";
                    Console.WriteLine("Colored but wrong!");
                }
            });
        }
    }
}
