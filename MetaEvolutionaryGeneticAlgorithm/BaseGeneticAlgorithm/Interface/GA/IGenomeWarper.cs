using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using System.Collections.Generic;

namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.GA
{
    public interface IGenomeWarper<T, U> where U : IGenomeWarper<T,U>
    {
        List<Genome> Cross(U other);
        void Challange(IEvaluationScenario<T> evalluationScenario);
    }
}
