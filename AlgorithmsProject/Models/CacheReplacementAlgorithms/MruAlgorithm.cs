using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.CacheReplacementAlgorithms
{
    /// <summary>
    /// Most Recently Used algorithm for cache replacement
    /// </summary>
    public class MruAlgorithm : CacheReplacementAlgorithm
    {
        #region Constructors

        public MruAlgorithm(int cacheSize) : base(cacheSize)
        {

        }

        #endregion

        #region Methods

        public override List<int> PagesToEvict()
        {
            var mostRecentlyAccessedIndex = Cache.First().Value.LastAccessIndex;
            int? pageToEvict = null;

            foreach (var item in Cache)
            {
                if (item.Value.LastAccessIndex >= mostRecentlyAccessedIndex)
                {
                    mostRecentlyAccessedIndex = item.Value.LastAccessIndex;
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
