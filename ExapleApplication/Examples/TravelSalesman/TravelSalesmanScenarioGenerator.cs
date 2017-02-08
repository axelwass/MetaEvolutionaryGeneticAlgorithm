using AlgoritmoGeneticoAutoevolutivo.Common;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApplication.Examples.TravelSalesman
{
    class TravelSalesmanScenarioGenerator : IEvaluationScenarioGenerator<TravelSalesmanIndividual>
    {
        int Nodes;
        TravelSalesmanScenario Individual;
        public float[,] DistancesVector;

        public TravelSalesmanScenarioGenerator(int nodes, float maxPathLength)
        {
            Nodes = nodes;

            DistancesVector = new float[Nodes, Nodes];

            for (int i = 0; i < Nodes; i++)
            {
                for (int j = 0; j < Nodes; j++)
                {
                    DistancesVector[i, j] = DistancesVector[j, i] = RandomGenerator.GetInstance().GetRandom(0, maxPathLength);
                }
            }

            Individual = new TravelSalesmanScenario(DistancesVector, maxPathLength);
        }

        public IEvaluationScenario<TravelSalesmanIndividual> GenerateEvaluationScenario()
        {
            return Individual;
        }
    }
}
