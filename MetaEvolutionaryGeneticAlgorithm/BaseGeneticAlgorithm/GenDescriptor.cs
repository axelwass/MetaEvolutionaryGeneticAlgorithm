using System;

namespace AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm
{
    class GenDescriptor
    {
        float Min;
        float Max;
        Random RandomGenerator;

        public GenDescriptor(float min, float max)
        {
            Min = min;
            Max = max;
            RandomGenerator = new Random();
        }

        public float GetRandom()
        {
            return Min + (RandomGenerator.Next() / int.MaxValue) * (Max-Min) ;
        }

        public float Mutate(int type, float value, float choose_probability, float amplitude_porcentage)
        {
            if((RandomGenerator.Next() /int.MaxValue) > choose_probability)
            {
                float min = value - (amplitude_porcentage / 2);
                min = Min > min ? Min : min;

                float max = value + (amplitude_porcentage / 2);
                max = Max < max ? Max : max;

                return min + (RandomGenerator.Next() / int.MaxValue) * (max - min);
            }
            return value;
        }

        public float Apariate(int type, float value1, float value2, float dominance_porcentage)
        {
            return (RandomGenerator.Next() / int.MaxValue) < dominance_porcentage ? value1 : value2;
        }
    }
}
