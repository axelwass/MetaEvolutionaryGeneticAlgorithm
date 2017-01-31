using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System.Collections.Generic;
using System.Linq;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary
{
    public class AutoEvolutionaryInformationFabrik<T> : IIndividualFabrik<AutoevolutionaryInformation> where T : IAutoEvolutionaryFitnessMatcher
    {
        IAutoEvolutionaryFitnessMatcherFabrik<T> FitnessMatcherFabrik;
        List<GenDescriptor> AutoEvolutionaryInformationGeneticDescriptor = new List<GenDescriptor> { new GenDescriptor(0, 1), new GenDescriptor(0, 1), new GenDescriptor(0, 1) };

        public AutoEvolutionaryInformationFabrik(IAutoEvolutionaryFitnessMatcherFabrik<T> fitnessMatcherFabrik)
        {
            FitnessMatcherFabrik = fitnessMatcherFabrik;
        }

        public List<GenDescriptor> GetGeneticDescriptor()
        {
            var descriptor = AutoEvolutionaryInformationGeneticDescriptor.ToList();
            descriptor.AddRange(FitnessMatcherFabrik.GetGeneticDescriptor());
            return descriptor;
        }

        public int GetGenCount()
        {
            return AutoEvolutionaryInformationGeneticDescriptor.Count + FitnessMatcherFabrik.GetGenCount();
        }

        public AutoevolutionaryInformation Create(List<Gen> gens)
        {
            return new AutoevolutionaryInformation(FitnessMatcherFabrik.Create(gens.Skip(AutoEvolutionaryInformationGeneticDescriptor.Count).ToList()), gens[0].Value, gens[1].Value, gens[2].Value);
        }
    }
}
