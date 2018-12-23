using AlgorithmsProject.Models.Benchmarks;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Properties

        private ObservableCollection<int> _cacheSizes;
        public ObservableCollection<int> CacheSizes
        {
            get { return _cacheSizes; }
            set { SetProperty(ref _cacheSizes, value); }
        }

        private ObservableCollection<int> _sequenceSizeMultipliers;
        public ObservableCollection<int> SequenceSizeMultipliers
        {
            get { return _sequenceSizeMultipliers; }
            set { SetProperty(ref _sequenceSizeMultipliers, value); }
        }

        private ObservableCollection<Tuple<string, Func<int, int>>> _distinctPagesCountGenerators;
        public ObservableCollection<Tuple<string, Func<int, int>>> DistinctPagesCountGenerators
        {
            get { return _distinctPagesCountGenerators; }
            set { SetProperty(ref _distinctPagesCountGenerators, value); }
        }

        private int _eachSettingIterations;
        public int EachSettingIterations
        {
            get { return _eachSettingIterations; }
            set { SetProperty(ref _eachSettingIterations, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (SetProperty(ref _isBusy, value))
                    RunBenchmarkCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<GroupedSequenceBenchmarkResult> _results;
        public ObservableCollection<GroupedSequenceBenchmarkResult> Results
        {
            get { return _results; }
            set { SetProperty(ref _results, value); }
        }

        #endregion

        #region Commands

        public DelegateCommand RunBenchmarkCommand { get; }

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            _cacheSizes = new ObservableCollection<int>(new int[] { 20, 40, 60, 80, 100 });
            _sequenceSizeMultipliers = new ObservableCollection<int>(new int[] { 100, 200, 300 });
            _distinctPagesCountGenerators = new ObservableCollection<Tuple<string, Func<int, int>>>(
                new Tuple<string, Func<int, int>>[] {
                    new Tuple<string, Func<int, int>>("x+2", x => x + 2),
                    new Tuple<string, Func<int, int>>("x*1.5", x => (int)(x * 1.5)),
                    new Tuple<string, Func<int, int>>("x*2", x => x * 2),
                    new Tuple<string, Func<int, int>>("x*3", x => x * 3) });
            _eachSettingIterations = 1000;

            RunBenchmarkCommand = new DelegateCommand(RunBenchmark, CanRunBenchmark);
        }

        #endregion

        #region Command Functions

        public bool CanRunBenchmark()
        {
            return !IsBusy;
        }

        public async void RunBenchmark()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            
            var results = await Task.Run(() =>
            {
                var fullBenchmark = new FullBenchmark(CacheSizes,
                SequenceSizeMultipliers,
                DistinctPagesCountGenerators.Select(d => d.Item2),
                EachSettingIterations);

                return fullBenchmark.RunBenchmarkForAllAlgorithms();
            });
            
            Results = new ObservableCollection<GroupedSequenceBenchmarkResult>(results);

            IsBusy = false;
        }

        #endregion
    }
}
