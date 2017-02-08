using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApplication.Examples.TravelSalesman
{
    public class TravelSalesmanFitness : IFitness
    {
        public float TotalDistance;
        int TravelOrderCount;
        float MaxPathLength;
        
        public TravelSalesmanFitness(float totalDistance, int travelOrderCount, float maxPathLength)
        {
            MaxPathLength = maxPathLength;
            TotalDistance = totalDistance;
            TravelOrderCount = travelOrderCount;
        }

        public float GetNormalized()
        {
            return 1 - TotalDistance / ((TravelOrderCount - 1) * MaxPathLength);
        }
    }
}
