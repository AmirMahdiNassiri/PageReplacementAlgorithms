using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.CacheReplacementAlgorithms
{
    public class Lru2Algorithm : CacheReplacementAlgorithm
    {
        #region Constructors

        public Lru2Algorithm(int cacheSize) : base(cacheSize)
        {

        }

        #endregion

        #region Methods

        public override List<int> PagesToEvict()
        {
            int? pageToEvict = null;

            if (Cache.Count < 2)
                pageToEvict = Cache.OrderBy(p => p.Value.LastAccessIndex).ToList().First().Key;
            else
                pageToEvict = Cache.OrderBy(p => p.Value.LastAccessIndex).ToList()[1].Key;

            if (pageToEvict.HasValue)
                return new List<int> { pageToEvict.Value };
            else
                throw new NotImplementedException();
        }

        #endregion
    }
}
