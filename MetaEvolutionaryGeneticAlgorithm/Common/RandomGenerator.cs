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

        public static RandomGenerator getInstance()
        {
            return Instance;
        }

        private RandomGenerator()
        {
            RandomNumbers = new Random();
        }

        public float getRandom (float min, float max)
        {
            return min + ((float)RandomNumbers.Next() / int.MaxValue) * (max - min);
        }
    }
}
