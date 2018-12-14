using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.CacheReplacementAlgorithms
{
    /// <summary>
    /// Least Recently Used algorithm for cache replacement
    /// </summary>
    public class LruAlgorithm : CacheReplacementAlgorithm
    {
        #region Constructors

        public LruAlgorithm(int cacheSize) : base(cacheSize)
        {

        }

        #endregion

        #region Methods
        
        public override List<int> PagesToEvict()
        {
            var leastRecentlyAccessedIndex = Cache.First().Value.LastAccessIndex;
            int? pageToEvict = null;

            foreach (var item in Cache)
            {
                if(item.Value.LastAccessIndex <= leastRecentlyAccessedIndex)
                {
                    leastRecentlyAccessedIndex = item.Value.LastAccessIndex;
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
