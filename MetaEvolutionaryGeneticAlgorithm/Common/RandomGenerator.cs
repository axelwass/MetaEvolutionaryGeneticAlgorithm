using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoGeneticoAutoevolutivo.Common
{
    public class RandomGenerator
    {
        static RandomGenerator Instance = new RandomGenerator();
        Random RandomNumbers;

        public static RandomGenerator GetInstance()
        {
            return Instance;
        }

        private RandomGenerator()
        {
            RandomNumbers = new Random();
        }

        public float GetRandom (float min, float max)
        {
            return min + ((float)RandomNumbers.Next() / int.MaxValue) * (max - min);
        }
    }
}
