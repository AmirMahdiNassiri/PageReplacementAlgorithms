using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.CacheReplacementAlgorithms
{
    /// <summary>
    /// Bit Pseudo Least Recently Used algorithm for cache replacement
    /// </summary>
    public class BitPlruAlgorithm : CacheReplacementAlgorithm
    {
        #region Constructors

        public BitPlruAlgorithm(int cacheSize) : base(cacheSize)
        {

        }

        #endregion

        #region Methods
        
        public override int HandleSingleInput(int input)
        {
            if (Cache.ContainsKey(input))
            {
                Cache[input].Accessed(CurrentIndexInSequence);
            }
            else if (!IsCacheFull)
            {
                Cache.Add(input, new PlruLoadedPageProperties(input, CurrentIndexInSequence));
                CurrentCost++;
            }
            else
            {
                foreach (var toEvict in PagesToEvict())
                {
                    if (!Cache.Remove(toEvict))
                        throw new InvalidOperationException("PageToEvict function returned a value which was not in the cache.");
                }

                Cache.Add(input, new PlruLoadedPageProperties(input, CurrentIndexInSequence));
                CurrentCost++;
            }

            if (IsCacheFull && Cache.All(p => ((PlruLoadedPageProperties)p.Value).Bit))
            {
                foreach (var item in Cache)
                {
                    if (item.Key != input)
                        ((PlruLoadedPageProperties)item.Value).Bit = false;
                }
            }

            CurrentIndexInSequence++;

            return CurrentCost;
        }
        
        public override List<int> PagesToEvict()
        {
            return new List<int> { Cache.First(p => ((PlruLoadedPageProperties)p.Value).Bit == false).Key };
        }

        #endregion
    }
}
