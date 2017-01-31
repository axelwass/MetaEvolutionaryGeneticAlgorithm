using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMatchers.DefaultImplementation
{
    public class AutoevolutionaryFitnessMatcherDefaultImplementationFabrik : IAutoEvolutionaryFitnessMatcherFabrik<AutoevolutionaryFitnessMatcherDefaultImplementation>
    {
        public AutoevolutionaryFitnessMatcherDefaultImplementation Create(List<Gen> gens)
        {
            return new AutoevolutionaryFitnessMatcherDefaultImplementation();
        }

        public int GetGenCount()
        {
            return 0;
        }

        public List<GenDescriptor> GetGeneticDescriptor()
        {
            return new List<GenDescriptor> { };
        }
    }
}
