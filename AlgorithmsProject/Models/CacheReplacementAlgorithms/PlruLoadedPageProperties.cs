using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.CacheReplacementAlgorithms
{
    public class PlruLoadedPageProperties : LoadedPageProperties
    {
        #region Properties

        public bool Bit { get; set; } = true;

        #endregion

        #region Constructors

        public PlruLoadedPageProperties(int page, int entranceIndex) : base(page, entranceIndex)
        {

        }

        #endregion

        #region Methods

        public override void Accessed(int newAccessIndexInSequence)
        {
            base.Accessed(newAccessIndexInSequence);

            Bit = true;
        }

        #endregion
    }
}
