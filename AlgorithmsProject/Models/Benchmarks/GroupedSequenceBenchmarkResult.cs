using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.Benchmarks
{
    public class GroupedSequenceBenchmarkResult
    {
        #region Properties

        public int CacheSize { get; }
        public int SequenceSizeMultiplier { get; }
        public int DistinctPagesCount { get; }

        public int FifoAnomalyCountOverall { get; set; }
        public int FifoAnomalousSequencesCount { get; set; }
        public double FifoCostAvg { get; set; }
        public double LargerFifoCostAvg { get; set; }

        public int Fifo2AnomalyCountOverall { get; set; }
        public int Fifo2AnomalousSequencesCount { get; set; }
        public double Fifo2CostAvg { get; set; }
        public double LargerFifo2CostAvg { get; set; }

        public int LruAnomalyCountOverall { get; set; }
        public int LruAnomalousSequencesCount { get; set; }
        public double LruCostAvg { get; set; }
        public double LargerLruCostAvg { get; set; }

        public int Lru2AnomalyCountOverall { get; set; }
        public int Lru2AnomalousSequencesCount { get; set; }
        public double Lru2CostAvg { get; set; }
        public double LargerLru2CostAvg { get; set; }

        public int MruAnomalyCountOverall { get; set; }
        public int MruAnomalousSequencesCount { get; set; }
        public double MruCostAvg { get; set; }
        public double LargerMruCostAvg { get; set; }

        public int BitPlruAnomalyCountOverall { get; set; }
        public int BitPlruAnomalousSequencesCount { get; set; }
        public double BitPlruCostAvg { get; set; }
        public double LargerBitPlruCostAvg { get; set; }

        public int FwfAnomalyCountOverall { get; set; }
        public int FwfAnomalousSequencesCount { get; set; }
        public double FwfCostAvg { get; set; }
        public double LargerFwfCostAvg { get; set; }

        public int LifoAnomalyCountOverall { get; set; }
        public int LifoAnomalousSequencesCount { get; set; }
        public double LifoCostAvg { get; set; }
        public double LargerLifoCostAvg { get; set; }

        public int UniformRandomPageAnomalyCountOverall { get; set; }
        public int UniformRandomPageAnomalousSequencesCount { get; set; }
        public double UniformRandomPageCostAvg { get; set; }
        public double LargerUniformRandomPageCostAvg { get; set; }

        #endregion

        #region Constructors

        public GroupedSequenceBenchmarkResult(int cacheSize, int sequenceSizeMultiplier,
            int distinctPagesCount)
        {
            CacheSize = cacheSize;
            SequenceSizeMultiplier = sequenceSizeMultiplier;
            DistinctPagesCount = distinctPagesCount;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"GroupedSequenceBenchmarkResult - CacheSize {CacheSize}, SequenceSizeMultiplier {SequenceSizeMultiplier}, DistinctPagesCount {DistinctPagesCount}";
        }

        #endregion
    }
}
