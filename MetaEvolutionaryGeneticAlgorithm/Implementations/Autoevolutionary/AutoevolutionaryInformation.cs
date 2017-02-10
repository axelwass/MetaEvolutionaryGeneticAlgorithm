using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System.Collections.Generic;
using System;
using System.Linq;
using MetaEvolutionaryGeneticAlgorithm.Common;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary
{
    public class AutoevolutionaryInformation
    {
        public IAutoEvolutionaryFitnessMatcher FitnessMatcher { get; }
        public float MutateGenChooseProbability { get; }
        public float MutateGenAmplitudePorcentage { get; }
        public float ApariateGenDominancePorcentage { get; }

        public List<float> MutationTypeProbability;
        public List<float> ApariationTypeProbability;

        public AutoevolutionaryInformation(IAutoEvolutionaryFitnessMatcher fitnessMatcher, float chooseProbability, float amplitudePorcentage, float dominancePorcentage, List<float> mutationTypeProbabilit, List<float> apariationTypeProbabilit)
        {
            FitnessMatcher = fitnessMatcher;
            MutateGenChooseProbability = chooseProbability;
            MutateGenAmplitudePorcentage = amplitudePorcentage;
            ApariateGenDominancePorcentage = dominancePorcentage;
            MutationTypeProbability = mutationTypeProbabilit;
            ApariationTypeProbability = apariationTypeProbabilit;
        }

        internal int GetMutationType()
        {
            float total = MutationTypeProbability.Sum();
            float selector = RandomGenerator.GetInstance().GetRandom(0, total);
            float sum = 0;
            int index = 0;
            foreach(float prov in MutationTypeProbability)
            {
                sum += prov;
                if (sum >= selector)
                {
                    return index; 
                }
                index++;
            }
            return 0;
        }

        internal int GetApariationType()
        {
            float total = ApariationTypeProbability.Sum();
            float selector = RandomGenerator.GetInstance().GetRandom(0, total);
            float sum = 0;
            int index = 0;
            foreach (float prov in ApariationTypeProbability)
            {
                sum += prov;
                if (sum >= selector)
                {
                    return index;
                }
                index++;
            }
            return 0;
        }
    }
}
