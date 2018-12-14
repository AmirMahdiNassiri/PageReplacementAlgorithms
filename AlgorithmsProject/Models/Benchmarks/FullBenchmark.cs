using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.Benchmarks
{
    public class FullBenchmark
    {
        #region Properties

        public List<int> CacheSizes { get; }
        public List<int> SequenceSizeMultipliers { get; }
        public List<Func<int, int>> DistinctPagesCountGenerators { get; }
        public int EachSettingIterations { get; }

        public List<GroupedSequenceBenchmarkResult> Result { get; private set; }

        #endregion

        #region Constructors

        public FullBenchmark() : this(new int[] { 20, 40, 60, 80, 100 },
            new int[] { 100, 200, 300 },
            new Func<int, int>[] { x => x + 2, x => (int)(x * 1.5), x => x * 2, x => x * 3 },
            1000)
        {

        }

        public FullBenchmark(IEnumerable<int> cacheSizes, 
            IEnumerable<int> sequenceSizeMultipliers, 
            IEnumerable<Func<int,int>> distinctPagesCountGenerators,
            int eachSettingIterations)
        {
            CacheSizes = new List<int>(cacheSizes);
            SequenceSizeMultipliers = new List<int>(sequenceSizeMultipliers);
            DistinctPagesCountGenerators = new List<Func<int, int>>(distinctPagesCountGenerators);
            EachSettingIterations = eachSettingIterations;
        }

        #endregion
        
        #region Methods

        public List<GroupedSequenceBenchmarkResult> RunBenchmarkForAllAlgorithms()
        {
            var singleResults = new ConcurrentBag<SingleSequenceBenchmarkResult>();
            
            foreach (var cacheSize in CacheSizes)
            {
                foreach (var sequenceSizeMultiplier in SequenceSizeMultipliers)
                {
                    foreach (var distinctPagesCountGenerator in DistinctPagesCountGenerators)
                    {
                        Parallel.For(0, EachSettingIterations, i =>
                          {
                              var currentBenchmark = new SingleSequenceBenchmark(cacheSize,
                                  sequenceSizeMultiplier, distinctPagesCountGenerator(cacheSize));

                              singleResults.Add(currentBenchmark.RunBenchmarkForAllAlgorithms());
                          });
                    }
                }
            }

            Result = new List<GroupedSequenceBenchmarkResult>();

            var cacheSizeGroups = singleResults.GroupBy(s => s.CacheSize);

            foreach (var cacheSizeGroup in cacheSizeGroups)
            {
                var sequenceSizeGroups = cacheSizeGroup.GroupBy(s => s.SequenceSizeMultiplier);

                foreach (var sequenceSizeGroup in sequenceSizeGroups)
                {
                    var distinctPagesCountGroups = sequenceSizeGroup.GroupBy(s => s.DistinctPagesCount);

                    foreach (var distinctPagesCountGroup in distinctPagesCountGroups)
                    {
                        var groupedResult = new GroupedSequenceBenchmarkResult(cacheSizeGroup.Key,
                            sequenceSizeGroup.Key, distinctPagesCountGroup.Key)
                        {
                            FifoAnomalyCountOverall = distinctPagesCountGroup.Sum(s => s.FifoAnomalyCount),
                            FifoAnomalousSequencesCount = distinctPagesCountGroup.Sum(s => s.FifoAnomalyFlag ? 1 : 0),
                            FifoCostAvg = distinctPagesCountGroup.Average(s => s.FifoCost),
                            LargerFifoCostAvg = distinctPagesCountGroup.Average(s => s.LargerFifoCost),

                            Fifo2AnomalyCountOverall = distinctPagesCountGroup.Sum(s => s.Fifo2AnomalyCount),
                            Fifo2AnomalousSequencesCount = distinctPagesCountGroup.Sum(s => s.Fifo2AnomalyFlag ? 1 : 0),
                            Fifo2CostAvg = distinctPagesCountGroup.Average(s => s.Fifo2Cost),
                            LargerFifo2CostAvg = distinctPagesCountGroup.Average(s => s.LargerFifo2Cost),

                            LruAnomalyCountOverall = distinctPagesCountGroup.Sum(s => s.LruAnomalyCount),
                            LruAnomalousSequencesCount = distinctPagesCountGroup.Sum(s => s.LruAnomalyFlag ? 1 : 0),
                            LruCostAvg = distinctPagesCountGroup.Average(s => s.LruCost),
                            LargerLruCostAvg = distinctPagesCountGroup.Average(s => s.LargerLruCost),

                            Lru2AnomalyCountOverall = distinctPagesCountGroup.Sum(s => s.Lru2AnomalyCount),
                            Lru2AnomalousSequencesCount = distinctPagesCountGroup.Sum(s => s.Lru2AnomalyFlag ? 1 : 0),
                            Lru2CostAvg = distinctPagesCountGroup.Average(s => s.Lru2Cost),
                            LargerLru2CostAvg = distinctPagesCountGroup.Average(s => s.LargerLru2Cost),

                            MruAnomalyCountOverall = distinctPagesCountGroup.Sum(s => s.MruAnomalyCount),
                            MruAnomalousSequencesCount = distinctPagesCountGroup.Sum(s => s.MruAnomalyFlag ? 1 : 0),
                            MruCostAvg = distinctPagesCountGroup.Average(s => s.MruCost),
                            LargerMruCostAvg = distinctPagesCountGroup.Average(s => s.LargerMruCost),

                            BitPlruAnomalyCountOverall = distinctPagesCountGroup.Sum(s => s.BitPlruAnomalyCount),
                            BitPlruAnomalousSequencesCount = distinctPagesCountGroup.Sum(s => s.BitPlruAnomalyFlag ? 1 : 0),
                            BitPlruCostAvg = distinctPagesCountGroup.Average(s => s.BitPlruCost),
                            LargerBitPlruCostAvg = distinctPagesCountGroup.Average(s => s.LargerBitPlruCost),

                            FwfAnomalyCountOverall = distinctPagesCountGroup.Sum(s => s.FwfAnomalyCount),
                            FwfAnomalousSequencesCount = distinctPagesCountGroup.Sum(s => s.FwfAnomalyFlag ? 1 : 0),
                            FwfCostAvg = distinctPagesCountGroup.Average(s => s.FwfCost),
                            LargerFwfCostAvg = distinctPagesCountGroup.Average(s => s.LargerFwfCost),

                            LifoAnomalyCountOverall = distinctPagesCountGroup.Sum(s => s.LifoAnomalyCount),
                            LifoAnomalousSequencesCount = distinctPagesCountGroup.Sum(s => s.LifoAnomalyFlag ? 1 : 0),
                            LifoCostAvg = distinctPagesCountGroup.Average(s => s.LifoCost),
                            LargerLifoCostAvg = distinctPagesCountGroup.Average(s => s.LargerLifoCost),

                            UniformRandomPageAnomalyCountOverall = distinctPagesCountGroup.Sum(s => s.UniformRandomPageAnomalyCount),
                            UniformRandomPageAnomalousSequencesCount = distinctPagesCountGroup.Sum(s => s.UniformRandomPageAnomalyFlag ? 1 : 0),
                            UniformRandomPageCostAvg = distinctPagesCountGroup.Average(s => s.UniformRandomPageCost),
                            LargerUniformRandomPageCostAvg = distinctPagesCountGroup.Average(s => s.LargerUniformRandomPageCost),
                        };

                        Result.Add(groupedResult);
                    }
                }
            }

            return Result;
        }

        #endregion
    }
}
