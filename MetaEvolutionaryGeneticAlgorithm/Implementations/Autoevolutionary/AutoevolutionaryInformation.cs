using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary
{
    public class AutoevolutionaryInformation
    {
        public IAutoEvolutionaryFitnessMatcher FitnessMatcher { get; }
        public float MutateGenChooseProbability { get; }
        public float MutateGenAmplitudePorcentage { get; }
        public float ApariateGenDominancePorcentage { get; }

        public AutoevolutionaryInformation(IAutoEvolutionaryFitnessMatcher fitnessMatcher, float chooseProbability, float amplitudePorcentage, float dominancePorcentage)
        {
            FitnessMatcher = fitnessMatcher;
            MutateGenChooseProbability = chooseProbability;
            MutateGenAmplitudePorcentage = amplitudePorcentage;
            ApariateGenDominancePorcentage = dominancePorcentage;
        }

    }
}
