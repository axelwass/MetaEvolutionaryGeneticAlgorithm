using MetaEvolutionaryGeneticAlgorithm.Common;
using System;

namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm
{
    public class GenDescriptor
    {
        public float Min;
        public float Max;
        public float Epsilon;
        public string Desc;

        public GenDescriptor(string desc,float min, float max, float epsilon)
        {
            Min = min;
            Max = max;
            Desc = desc;
            Epsilon = epsilon;
        }

        public float GetRandom()
        {
            return RandomGenerator.GetInstance().GetRandom(Min,Max);
        }
    }
}
