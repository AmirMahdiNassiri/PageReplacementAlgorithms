using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.CacheReplacementAlgorithms
{
    public class UniformRandomPageAlgorithm : CacheReplacementAlgorithm
    {
        #region Properties

        public Random RandomGenerator { get; }
        public List<double> RandomNumbers { get; }
        public int CurrentIndexInRandomNumbers { get; private set; }

        #endregion

        #region Constructors

        public UniformRandomPageAlgorithm(int cacheSize) : base(cacheSize)
        {
            RandomGenerator = new Random();
            RandomNumbers = new List<double>();
        }

        public UniformRandomPageAlgorithm(int cacheSize, IEnumerable<double> randomNumbersToUse) :
            base(cacheSize)
        {
            RandomGenerator = new Random();
            RandomNumbers = new List<double>(randomNumbersToUse);
        }

        #endregion

        #region Methods

        private int GetNextEvictionIndex()
        {
            if(RandomNumbers.Count == CurrentIndexInRandomNumbers)
            {
                RandomNumbers.Add(RandomGenerator.Next(0, 10001) / 10000.0);
            }

            var result = (int)Math.Round(RandomNumbers[CurrentIndexInRandomNumbers] * (CacheSize - 1));

            CurrentIndexInRandomNumbers++;

            return result;
        }

        public override List<int> PagesToEvict()
        {
            int? pageToEvict = Cache.ToList()[GetNextEvictionIndex()].Key;

            if (pageToEvict.HasValue)
                return new List<int> { pageToEvict.Value };
            else
                throw new NotImplementedException();
        }

        #endregion
    }
}
