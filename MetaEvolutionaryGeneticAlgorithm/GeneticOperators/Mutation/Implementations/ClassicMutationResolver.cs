using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using MetaEvolutionaryGeneticAlgorithm.Common;

namespace MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Mutation.Implementations
{
    public class ClassicMutationResolver : IMutationResolver
    {
        public List<Gen> Mutate(List<Gen> gens, float chooseProbability, float amplitudePorcentage)
        {
            return gens.Select(g => new Gen(g.Descriptor, MutateGen(g.Value,g.Descriptor.Min, g.Descriptor.Max, chooseProbability, amplitudePorcentage))).ToList();
        }


        private float MutateGen(float value, float globalMin, float globalMax, float choose_probability, float amplitude_porcentage)
        {
            if (RandomGenerator.GetInstance().GetRandom(0, 1) < choose_probability)
            {
                float min = value - (amplitude_porcentage / 2);
                min = globalMin > min ? globalMin : min;

                float max = value + (amplitude_porcentage / 2);
                max = globalMax < max ? globalMax : max;

                return RandomGenerator.GetInstance().GetRandom(min, max);
            }
            return value;
        }
    }
}
