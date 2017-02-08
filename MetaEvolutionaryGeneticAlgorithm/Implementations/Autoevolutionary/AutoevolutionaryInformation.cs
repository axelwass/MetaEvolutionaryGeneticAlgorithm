using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System.Collections.Generic;
using System;
using System.Linq;
using AlgoritmoGeneticoAutoevolutivo.Common;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary
{
    public class AutoevolutionaryInformation
    {
        public IAutoEvolutionaryFitnessMatcher FitnessMatcher { get; }
        public float MutateGenChooseProbability { get; }
        public float MutateGenAmplitudePorcentage { get; }
        public float ApariateGenDominancePorcentage { get; }

        public List<float> TypeProbability;

        public AutoevolutionaryInformation(IAutoEvolutionaryFitnessMatcher fitnessMatcher, float chooseProbability, float amplitudePorcentage, float dominancePorcentage, List<float> typeProbability)
        {
            FitnessMatcher = fitnessMatcher;
            MutateGenChooseProbability = chooseProbability;
            MutateGenAmplitudePorcentage = amplitudePorcentage;
            ApariateGenDominancePorcentage = dominancePorcentage;
            TypeProbability = typeProbability;
        }

        internal int GetMutationType()
        {
            float total = TypeProbability.Sum();
            float selector = RandomGenerator.GetInstance().GetRandom(0, total);
            float sum = 0;
            int index = 0;
            foreach(float prov in TypeProbability)
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
