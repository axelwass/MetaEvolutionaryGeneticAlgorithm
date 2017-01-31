using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem
{
    public interface IEvaluationScenario<T>
    {

        IFitness Evaluate(T individuo);
    }
}
