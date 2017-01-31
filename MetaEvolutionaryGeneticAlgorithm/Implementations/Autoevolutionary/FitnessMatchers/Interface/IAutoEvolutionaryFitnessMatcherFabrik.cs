using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface
{
    public interface IAutoEvolutionaryFitnessMatcherFabrik<T> : IIndividualFabrik<T> where T : IAutoEvolutionaryFitnessMatcher
    {
    }
}
