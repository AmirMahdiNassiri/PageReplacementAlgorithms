using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.CacheReplacementAlgorithms
{
    /// <summary>
    /// First In First Out algorithm for cache replacement
    /// </summary>
    public class FifoAlgorithm : CacheReplacementAlgorithm
    {
        #region Constructors

        public FifoAlgorithm(int cacheSize) : base(cacheSize)
        {

        }

        #endregion

        #region Methods

        public override List<int> PagesToEvict()
        {
            var firstEnteredIndex = Cache.First().Value.EntranceIndex;
            int? pageToEvict = null;

            foreach (var item in Cache)
            {
                if (item.Value.EntranceIndex <= firstEnteredIndex)
                {
                    firstEnteredIndex = item.Value.EntranceIndex;
                    pageToEvict = item.Key;
                }
            }

            if (pageToEvict.HasValue)
                return new List<int> { pageToEvict.Value };
            else
                throw new NotImplementedException();
        }

        #endregion
    }
}
