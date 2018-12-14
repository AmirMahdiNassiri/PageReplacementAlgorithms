using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmsProject.Models.CacheReplacementAlgorithms;

namespace AlgorithmsProject.Models.Benchmarks
{
    public class SingleSequenceBenchmark
    {
        #region Properties

        public int CacheSize { get; }
        public int SequenceSizeMultiplier { get; }
        public int DistinctPagesCount { get; }
        public Random RandomGeneratorSequence { get; }
        public int[] InputSequence { get; }

        public FifoAlgorithm FifoAlgorithm { get; }
        public FifoAlgorithm LargerFifoAlgorithm { get; }

        public Fifo2Algorithm Fifo2Algorithm { get; }
        public Fifo2Algorithm LargerFifo2Algorithm { get; }

        public LruAlgorithm LruAlgorithm { get; }
        public LruAlgorithm LargerLruAlgorithm { get; }

        public Lru2Algorithm Lru2Algorithm { get; }
        public Lru2Algorithm LargerLru2Algorithm { get; }

        public MruAlgorithm MruAlgorithm { get; }
        public MruAlgorithm LargerMruAlgorithm { get; }

        public BitPlruAlgorithm BitPlruAlgorithm { get; }
        public BitPlruAlgorithm LargerBitPlruAlgorithm { get; }

        public FwfAlgorithm FwfAlgorithm { get; }
        public FwfAlgorithm LargerFwfAlgorithm { get; }

        public LifoAlgorithm LifoAlgorithm { get; }
        public LifoAlgorithm LargerLifoAlgorithm { get; }

        public UniformRandomPageAlgorithm UniformRandomPageAlgorithm { get; }
        public UniformRandomPageAlgorithm LargerUniformRandomPageAlgorithm { get; }

        #endregion

        #region Constructors

        public SingleSequenceBenchmark(int cacheSize, int sequenceSizeMultiplier, int distinctPagesCount)
        {
            CacheSize = cacheSize;
            SequenceSizeMultiplier = sequenceSizeMultiplier;
            DistinctPagesCount = distinctPagesCount;
            RandomGeneratorSequence = new Random();

            InputSequence = new int[CacheSize * SequenceSizeMultiplier];

            for (int i = 0; i < InputSequence.Length; i++)
            {
                InputSequence[i] = RandomGeneratorSequence.Next(distinctPagesCount + 1);
            }

            #region Initializing Algorithms

            FifoAlgorithm = new FifoAlgorithm(cacheSize);
            LargerFifoAlgorithm = new FifoAlgorithm(cacheSize + 1);

            Fifo2Algorithm = new Fifo2Algorithm(cacheSize);
            LargerFifo2Algorithm = new Fifo2Algorithm(cacheSize + 1);

            LruAlgorithm = new LruAlgorithm(cacheSize);
            LargerLruAlgorithm = new LruAlgorithm(cacheSize + 1);

            Lru2Algorithm = new Lru2Algorithm(cacheSize);
            LargerLru2Algorithm = new Lru2Algorithm(cacheSize + 1);

            MruAlgorithm = new MruAlgorithm(cacheSize);
            LargerMruAlgorithm = new MruAlgorithm(cacheSize + 1);

            BitPlruAlgorithm = new BitPlruAlgorithm(cacheSize);
            LargerBitPlruAlgorithm = new BitPlruAlgorithm(cacheSize + 1);

            FwfAlgorithm = new FwfAlgorithm(cacheSize);
            LargerFwfAlgorithm = new FwfAlgorithm(cacheSize + 1);

            LifoAlgorithm = new LifoAlgorithm(cacheSize);
            LargerLifoAlgorithm = new LifoAlgorithm(cacheSize + 1);

            UniformRandomPageAlgorithm = new UniformRandomPageAlgorithm(cacheSize);
            LargerUniformRandomPageAlgorithm = new UniformRandomPageAlgorithm(cacheSize + 1);

            #endregion
        }

        #endregion

        #region Methods

        public SingleSequenceBenchmarkResult RunBenchmarkForAllAlgorithms()
        {
            var result = new SingleSequenceBenchmarkResult(CacheSize,
                SequenceSizeMultiplier, DistinctPagesCount);

            for (int i = 0; i < InputSequence.Length; i++)
            {
                var currentInput = InputSequence[i];

                if (LargerFifoAlgorithm.HandleSingleInput(currentInput) >
                    FifoAlgorithm.HandleSingleInput(currentInput))
                    result.FifoAnomalyCount++;
                
                if (LargerFifo2Algorithm.HandleSingleInput(currentInput) >
                    Fifo2Algorithm.HandleSingleInput(currentInput))
                    result.Fifo2AnomalyCount++;

                if (LargerLruAlgorithm.HandleSingleInput(currentInput) >
                    LruAlgorithm.HandleSingleInput(currentInput))
                    result.LruAnomalyCount++;

                if (LargerLru2Algorithm.HandleSingleInput(currentInput) >
                    Lru2Algorithm.HandleSingleInput(currentInput))
                    result.Lru2AnomalyCount++;

                if (LargerMruAlgorithm.HandleSingleInput(currentInput) >
                    MruAlgorithm.HandleSingleInput(currentInput))
                    result.MruAnomalyCount++;

                if (LargerBitPlruAlgorithm.HandleSingleInput(currentInput) >
                    BitPlruAlgorithm.HandleSingleInput(currentInput))
                    result.BitPlruAnomalyCount++;

                if (LargerFwfAlgorithm.HandleSingleInput(currentInput) >
                    FwfAlgorithm.HandleSingleInput(currentInput))
                    result.FwfAnomalyCount++;

                if (LargerLifoAlgorithm.HandleSingleInput(currentInput) >
                    LifoAlgorithm.HandleSingleInput(currentInput))
                    result.LifoAnomalyCount++;

                {
                    var largerPreviousRandomNumbersCount = LargerUniformRandomPageAlgorithm.
                        RandomNumbers.Count;

                    var largerUniformRandomPageCost = LargerUniformRandomPageAlgorithm.
                        HandleSingleInput(currentInput);

                    if(LargerUniformRandomPageAlgorithm.RandomNumbers.Count >
                        largerPreviousRandomNumbersCount)
                    {
                        UniformRandomPageAlgorithm.RandomNumbers.Add(
                            LargerUniformRandomPageAlgorithm.RandomNumbers.Last());
                        
                        Debug.Assert(UniformRandomPageAlgorithm.RandomNumbers.Count ==
                            LargerUniformRandomPageAlgorithm.RandomNumbers.Count,
                            "Error in retaining random numbers between two algorithms.");
                    }

                    var previousRandomNumbersCount = UniformRandomPageAlgorithm.
                        RandomNumbers.Count;

                    var uniformRandomPageCost = UniformRandomPageAlgorithm.HandleSingleInput(currentInput);

                    if (UniformRandomPageAlgorithm.RandomNumbers.Count >
                       previousRandomNumbersCount)
                    {
                        LargerUniformRandomPageAlgorithm.RandomNumbers.Add(
                            UniformRandomPageAlgorithm.RandomNumbers.Last());
                        
                        Debug.Assert(UniformRandomPageAlgorithm.RandomNumbers.Count ==
                            LargerUniformRandomPageAlgorithm.RandomNumbers.Count,
                            "Error in retaining random numbers between two algorithms.");
                    }

                    if (largerUniformRandomPageCost > uniformRandomPageCost)
                        result.UniformRandomPageAnomalyCount++;
                }
            }

            result.FifoCost = FifoAlgorithm.CurrentCost;
            result.LargerFifoCost = LargerFifoAlgorithm.CurrentCost;

            result.Fifo2Cost = Fifo2Algorithm.CurrentCost;
            result.LargerFifo2Cost = LargerFifo2Algorithm.CurrentCost;

            result.LruCost = LruAlgorithm.CurrentCost;
            result.LargerLruCost = LargerLruAlgorithm.CurrentCost;
            
            result.Lru2Cost = Lru2Algorithm.CurrentCost;
            result.LargerLru2Cost = LargerLru2Algorithm.CurrentCost;

            result.BitPlruCost = BitPlruAlgorithm.CurrentCost;
            result.LargerBitPlruCost = LargerBitPlruAlgorithm.CurrentCost;

            result.MruCost = MruAlgorithm.CurrentCost;
            result.LargerMruCost = LargerMruAlgorithm.CurrentCost;

            result.LifoCost = LifoAlgorithm.CurrentCost;
            result.LargerLifoCost = LargerLifoAlgorithm.CurrentCost;

            result.FwfCost = FwfAlgorithm.CurrentCost;
            result.LargerFwfCost = LargerFwfAlgorithm.CurrentCost;

            result.UniformRandomPageCost = UniformRandomPageAlgorithm.CurrentCost;
            result.LargerUniformRandomPageCost = LargerUniformRandomPageAlgorithm.CurrentCost;

            return result;
        }

        #endregion
    }
}
