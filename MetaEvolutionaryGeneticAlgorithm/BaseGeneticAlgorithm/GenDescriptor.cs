using AlgoritmoGeneticoAutoevolutivo.Common;
using System;

namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm
{
    public class GenDescriptor
    {
        float Min;
        float Max;

        public GenDescriptor(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float GetRandom()
        {
            return RandomGenerator.getInstance().getRandom(Min,Max);
        }

        public float Mutate(int type, float value, float choose_probability, float amplitude_porcentage)
        {
            if(RandomGenerator.getInstance().getRandom(0, 1) > choose_probability)
            {
                float min = value - (amplitude_porcentage / 2);
                min = Min > min ? Min : min;

                float max = value + (amplitude_porcentage / 2);
                max = Max < max ? Max : max;

                return RandomGenerator.getInstance().getRandom(min, max);
            }
            return value;
        }

        public float Apariate(int type, float value1, float value2, float dominance_porcentage)
        {
            return RandomGenerator.getInstance().getRandom(0, 1) < dominance_porcentage ? value1 : value2;
        }
    }
}
