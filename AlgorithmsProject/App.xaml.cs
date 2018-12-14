using AlgorithmsProject.Models.Benchmarks;
using AlgorithmsProject.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AlgorithmsProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            FullBenchmark f = new FullBenchmark();

            var fullBenchmarkResult = f.RunBenchmarkForAllAlgorithms();
            
            var window = new TestWindow(fullBenchmarkResult);

            window.Show();
        }
    }
}
