using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMatchers.DefaultImplementation
{
    public class AutoevolutionaryFitnessMatcherDefaultImplementation : IAutoEvolutionaryFitnessMatcher
    {
        public float Match(IFitness fitness1, IFitness fitness2)
        {
            return fitness2.GetNormalized();
        }
    }
}
