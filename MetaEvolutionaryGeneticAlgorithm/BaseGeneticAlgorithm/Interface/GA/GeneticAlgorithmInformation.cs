using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.GA
{
    public class GeneticAlgorithmInformation<T>
    {
        public IIndividualFabrik<T> IndividualFabrik;
        public IEvaluationScenarioGenerator<T> ScenarioGeneratior;
    }
}
