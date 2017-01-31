using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;

namespace ExampleApplication.Examples.TravelSalesman
{
    class TravelSalesmanIndividual
    {
        public List<int> travelOrder;

        public TravelSalesmanIndividual(List<int> travelOrder)
        {
            this.travelOrder = travelOrder;
        }

        internal IFitness Evaluate(float[,] distanceVector, float maxPathLength)
        {

            float totalDistance = 0;

            for(int i = 0; i < travelOrder.Count - 1; i++)
            {
                totalDistance += distanceVector[travelOrder[i], travelOrder[i + 1]];
            }

            return new TravelSalesmanFitness( 1 - totalDistance / ((travelOrder.Count - 1) * maxPathLength));
        }
    }
}
