using AlgoritmoGeneticoAutoevolutivo.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.Problem;

namespace AlgoritmoGeneticoAutoevolutivo.Implementations.Autoevolutionary.FitnessMatchers.DefaultImplementation
{
    class AutoevolutionaryFitnessMatcherDefaultImplementation : IAutoEvolutionaryFitnessMatcher
    {
        public float Match(IFitness fitness1, IFitness fitness2)
        {
            return fitness2.GetNormalized();
        }
    }
}
