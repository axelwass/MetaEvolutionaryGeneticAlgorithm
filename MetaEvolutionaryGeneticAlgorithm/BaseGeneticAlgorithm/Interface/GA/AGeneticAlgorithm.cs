using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.Problem;
using System.Collections.Generic;

namespace AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.GA
{
    abstract class AGeneticAlgorithm<T, V> where V:IGenomeWarper<T, V>
    {
        protected int GenerationNumber;
        protected IIndividualFabrik<T> IndividualFabrik;
        protected IEvaluationScenarioGenerator<T> ScenarioGeneratior;

        public List<V> Generation { get; protected set; }

        public AGeneticAlgorithm(IIndividualFabrik<T> individualFabrik, IEvaluationScenarioGenerator<T> scenarioGenerator)
        {
            IndividualFabrik = individualFabrik;
            ScenarioGeneratior = scenarioGenerator;
        }

        abstract public void AdvanceGenerations(int generations);
    }
}
