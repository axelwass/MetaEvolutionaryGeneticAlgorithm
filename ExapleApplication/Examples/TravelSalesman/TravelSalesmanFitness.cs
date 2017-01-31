using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApplication.Examples.TravelSalesman
{
    class TravelSalesmanFitness : IFitness
    {
        float Fitness;

        public TravelSalesmanFitness(float fitness)
        {
             Fitness = fitness;
        }

        public float GetNormalized()
        {
            return Fitness;
        }
    }
}
