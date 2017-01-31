using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface
{
    public interface IAutoEvolutionaryFitnessMatcher
    {
        float Match(IFitness fitness1, IFitness fitness2);
    }
}
