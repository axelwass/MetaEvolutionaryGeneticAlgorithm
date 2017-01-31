using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm;
using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.Problem;
using AlgoritmoGeneticoAutoevolutivo.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System.Collections.Generic;
using System.Linq;

namespace AlgoritmoGeneticoAutoevolutivo.Implementations.Autoevolutionary
{
    class AutoEvolutionaryInformationFabrik : IIndividualFabrik<AutoevolutionaryInformation>
    {
        IAutoEvolutionaryFitnessMatcherFabrik<IAutoEvolutionaryFitnessMatcher> FitnessMatcherFabrik;
        List<GenDescriptor> AutoEvolutionaryInformationGeneticDescriptor = new List<GenDescriptor> { new GenDescriptor(0, 1), new GenDescriptor(0, 1), new GenDescriptor(0, 1) };

        AutoEvolutionaryInformationFabrik(IAutoEvolutionaryFitnessMatcherFabrik<IAutoEvolutionaryFitnessMatcher> fitnessMatcherFabrik)
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
