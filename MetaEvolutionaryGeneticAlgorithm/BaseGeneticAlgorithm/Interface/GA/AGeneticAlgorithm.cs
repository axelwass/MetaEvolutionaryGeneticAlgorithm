using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using System.Collections.Generic;

namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.GA
{
    public abstract class AGeneticAlgorithm<T, U, V> where V:IGenomeWarper<T, V> where U : GeneticAlgorithmInformation<T>
    {
        public int GenerationNumber;
        protected U GeneticAlgorithmInformation;

        public List<V> Population { get; protected set; }

        public AGeneticAlgorithm(U geneticAlgorithmInformation)
        {
            GeneticAlgorithmInformation = geneticAlgorithmInformation;
        }

        abstract public void AdvanceGenerations(int generations);
    }
}
