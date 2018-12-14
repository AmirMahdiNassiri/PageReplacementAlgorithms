using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.Benchmarks
{
    public class SingleSequenceBenchmarkResult
    {
        #region Properties

        public int CacheSize { get; }
        public int SequenceSizeMultiplier { get; }
        public int DistinctPagesCount { get; }

        public int FifoAnomalyCount { get; set; }
        public bool FifoAnomalyFlag => FifoAnomalyCount > 0;
        public int FifoCost { get; set; }
        public int LargerFifoCost { get; set; }

        public int Fifo2AnomalyCount { get; set; }
        public bool Fifo2AnomalyFlag => Fifo2AnomalyCount > 0;
        public int Fifo2Cost { get; set; }
        public int LargerFifo2Cost { get; set; }

        public int LruAnomalyCount { get; set; }
        public bool LruAnomalyFlag => LruAnomalyCount > 0;
        public int LruCost { get; set; }
        public int LargerLruCost { get; set; }

        public int Lru2AnomalyCount { get; set; }
        public bool Lru2AnomalyFlag => Lru2AnomalyCount > 0;
        public int Lru2Cost { get; set; }
        public int LargerLru2Cost { get; set; }

        public int MruAnomalyCount { get; set; }
        public bool MruAnomalyFlag => MruAnomalyCount > 0;
        public int MruCost { get; set; }
        public int LargerMruCost { get; set; }

        public int BitPlruAnomalyCount { get; set; }
        public bool BitPlruAnomalyFlag => BitPlruAnomalyCount > 0;
        public int BitPlruCost { get; set; }
        public int LargerBitPlruCost { get; set; }

        public int FwfAnomalyCount { get; set; }
        public bool FwfAnomalyFlag => FwfAnomalyCount > 0;
        public int FwfCost { get; set; }
        public int LargerFwfCost { get; set; }

        public int LifoAnomalyCount { get; set; }
        public bool LifoAnomalyFlag => LifoAnomalyCount > 0;
        public int LifoCost { get; set; }
        public int LargerLifoCost { get; set; }

        public int UniformRandomPageAnomalyCount { get; set; }
        public bool UniformRandomPageAnomalyFlag => UniformRandomPageAnomalyCount > 0;
        public int UniformRandomPageCost { get; set; }
        public int LargerUniformRandomPageCost { get; set; }

        #endregion

        #region Constructors

        public SingleSequenceBenchmarkResult(int cacheSize, int sequenceSizeMultiplier, int distinctPagesCount)
        {
            CacheSize = cacheSize;
            SequenceSizeMultiplier = sequenceSizeMultiplier;
            DistinctPagesCount = distinctPagesCount;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            var result = string.Empty;

            if (FifoAnomalyFlag)
                result += "FIFO ";

            if (Fifo2AnomalyFlag)
                result += "FIFO-2 ";

            if (LruAnomalyFlag)
                result += "LRU ";

            if (Lru2AnomalyFlag)
                result += "LRU-2 ";

            if (MruAnomalyFlag)
                result += "MRU ";

            if (BitPlruAnomalyFlag)
                result += "BitPLRU ";

            if (FwfAnomalyFlag)
                result += "FWF ";

            if (LifoAnomalyFlag)
                result += "LIFO ";

            if (UniformRandomPageAnomalyFlag)
                result += "Random ";

            if (string.IsNullOrWhiteSpace(result))
                result = "No anomaly detected.";
            
            return result;
        }

        #endregion
    }
}
