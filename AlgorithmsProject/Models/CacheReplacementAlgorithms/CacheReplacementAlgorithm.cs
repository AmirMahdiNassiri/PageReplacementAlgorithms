using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsProject.Models.CacheReplacementAlgorithms
{
    public abstract class CacheReplacementAlgorithm
    {
        #region Properties

        public int CacheSize { get; }

        public Dictionary<int, LoadedPageProperties> Cache { get; }

        public bool IsCacheFull
        {
            get { return Cache.Count == CacheSize; }
        }

        public int CurrentCost { get; protected set; }
        public int CurrentIndexInSequence { get; protected set; }

        #endregion

        #region Constructors

        public CacheReplacementAlgorithm(int cacheSize)
        {
            CacheSize = cacheSize;

            Cache = new Dictionary<int, LoadedPageProperties>();
        }

        #endregion

        #region Methods

        public virtual int HandleSingleInput(int input)
        {
            if (Cache.ContainsKey(input))
            {
                Cache[input].Accessed(CurrentIndexInSequence);
            }
            else if (!IsCacheFull)
            {
                Cache.Add(input, new LoadedPageProperties(input, CurrentIndexInSequence));
                CurrentCost++;
            }
            else
            {
                foreach (var toEvict in PagesToEvict())
                {
                    if (!Cache.Remove(toEvict))
                        throw new InvalidOperationException("PageToEvict function returned a value which was not in the cache.");
                }

                Cache.Add(input, new LoadedPageProperties(input, CurrentIndexInSequence));
                CurrentCost++;
            }

            CurrentIndexInSequence++;

            return CurrentCost;
        }

        public int HandleSequence(int[] inputSequence)
        {
            for (int i = 0; i < inputSequence.Length; i++)
            {
                HandleSingleInput(inputSequence[i]);
            }
            
            return CurrentCost;
        }

        public int HandleSingleInput(char input)
        {
            var bytes = Encoding.ASCII.GetBytes(new char[] { input });

            byte[] temp = new byte[4];
            bytes.CopyTo(temp, 0);
            bytes = temp;

            var convertedInput = BitConverter.ToInt32(bytes, 0);
            
            return HandleSingleInput(convertedInput);
        }

        public int HandleSequence(string inputSequence)
        {
            var convertedInputSequence = new int[inputSequence.Length];

            for (int i = 0; i < inputSequence.Length; i++)
            {
                var bytes = Encoding.ASCII.GetBytes(new char[] { inputSequence[i] });

                byte[] temp = new byte[4];
                bytes.CopyTo(temp, 0);
                bytes = temp;

                convertedInputSequence[i] = BitConverter.ToInt32(bytes, 0);
            }

            return HandleSequence(convertedInputSequence);
        }

        public abstract List<int> PagesToEvict();

        #endregion
    }
}
