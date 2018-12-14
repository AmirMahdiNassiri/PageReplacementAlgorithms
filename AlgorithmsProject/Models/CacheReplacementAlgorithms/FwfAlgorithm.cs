using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.CacheReplacementAlgorithms
{
    /// <summary>
    /// Flash When Full algorithm for cache replacement
    /// </summary>
    public class FwfAlgorithm : CacheReplacementAlgorithm
    {
        #region Constructors

        public FwfAlgorithm(int cacheSize) : base(cacheSize)
        {

        }

        #endregion

        #region Methods

        public override List<int> PagesToEvict()
        {
            return Cache.Keys.ToList();
        }

        #endregion
    }
}
