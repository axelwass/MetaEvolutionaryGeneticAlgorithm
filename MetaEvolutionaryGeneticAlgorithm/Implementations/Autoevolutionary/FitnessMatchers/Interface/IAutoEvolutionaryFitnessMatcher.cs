using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.Problem;

namespace AlgoritmoGeneticoAutoevolutivo.Implementations.Autoevolutionary.FitnessMAtcher.Interface
{
    interface IAutoEvolutionaryFitnessMatcher
    {
        float Match(IFitness fitness1, IFitness fitness2);
    }
}
