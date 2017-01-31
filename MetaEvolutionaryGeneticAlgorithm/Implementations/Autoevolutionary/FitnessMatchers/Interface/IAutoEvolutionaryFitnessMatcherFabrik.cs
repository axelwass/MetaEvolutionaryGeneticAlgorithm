using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.Problem;

namespace AlgoritmoGeneticoAutoevolutivo.Implementations.Autoevolutionary.FitnessMAtcher.Interface
{
    interface IAutoEvolutionaryFitnessMatcherFabrik<T> : IIndividualFabrik<T> where T : IAutoEvolutionaryFitnessMatcher
    {
    }
}
