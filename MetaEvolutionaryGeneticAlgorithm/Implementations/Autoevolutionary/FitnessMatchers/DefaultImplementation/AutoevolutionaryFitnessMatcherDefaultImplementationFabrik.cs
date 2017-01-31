using AlgoritmoGeneticoAutoevolutivo.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm;

namespace AlgoritmoGeneticoAutoevolutivo.Implementations.Autoevolutionary.FitnessMatchers.DefaultImplementation
{
    class AutoevolutionaryFitnessMatcherDefaultImplementationFabrik : IAutoEvolutionaryFitnessMatcherFabrik<AutoevolutionaryFitnessMatcherDefaultImplementation>
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
