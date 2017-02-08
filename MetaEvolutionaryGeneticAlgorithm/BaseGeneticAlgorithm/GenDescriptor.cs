using AlgoritmoGeneticoAutoevolutivo.Common;
using System;

namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm
{
    public class GenDescriptor
    {
        public float Min;
        public float Max;
        public string Desc;

        public GenDescriptor(string desc,float min, float max)
        {
            Min = min;
            Max = max;
            Desc = desc;
        }

        public float GetRandom()
        {
            return RandomGenerator.GetInstance().GetRandom(Min,Max);
        }

        public float Apariate(int type, float value1, float value2, float dominance_porcentage)
        {
            return RandomGenerator.GetInstance().GetRandom(0, 1) < dominance_porcentage ? value1 : value2;
        }
    }
}
