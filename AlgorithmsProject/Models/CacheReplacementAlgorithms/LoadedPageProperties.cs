using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.CacheReplacementAlgorithms
{
    public class LoadedPageProperties
    {
        #region Properties

        public int Page { get; set; }

        public int EntranceIndex { get; set; }

        public int LastAccessIndex { get; set; }

        #endregion

        #region Constructors

        public LoadedPageProperties(int page, int entranceIndex)
        {
            Page = page;
            EntranceIndex = entranceIndex;
            LastAccessIndex = entranceIndex;
        }

        #endregion

        #region Methods

        public virtual void Accessed(int newAccessIndexInSequence)
        {
            LastAccessIndex = newAccessIndexInSequence;
        }

        #endregion
    }
}
