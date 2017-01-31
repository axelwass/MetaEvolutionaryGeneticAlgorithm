using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApplication.Examples.TravelSalesman
{
    class TravelSalesmanScenario : IEvaluationScenario<TravelSalesmanIndividual>
    {
        float[,] DistanceVector;
        float MaxPathLength;

        public TravelSalesmanScenario(float[,] distanceVector, float maxPathLength)
        {
            DistanceVector = distanceVector;
            MaxPathLength = maxPathLength;
        }

        public IFitness Evaluate(TravelSalesmanIndividual individuo)
        {
            return individuo.Evaluate(DistanceVector, MaxPathLength);
        }
    }
}
