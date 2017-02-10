using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using ExampleApplication.Examples.TravelSalesman;
using MetaEvolutionaryGeneticAlgorithm.Common;

namespace ExapleApplication.Examples.TravelSalesman
{
    class TravelSalesmanPathIndividualFabrik : IIndividualFabrik<TravelSalesmanIndividual>
    {
        int Nodes;

        public TravelSalesmanPathIndividualFabrik(int nodes)
        {
            Nodes = nodes;
        }

        public TravelSalesmanIndividual Create(List<Gen> gens)
        {
            return new TravelSalesmanIndividual(gens.Select(g => (int)g.Value).ToList());
        }

        public int GetGenCount()
        {
            return Nodes;
        }

        public List<GenDescriptor> GetGeneticDescriptor()
        {
            var gens = new List<GenDescriptor>();
            for (int i = 0; i < Nodes; i++)
            {
                gens.Add(new GenDescriptor("Next node", 0, Nodes, 0.5f));
            }
            return gens;
        }

        public List<Gen> GetRandomGenList()
        {
            int i = 0;
            var gens = GetGeneticDescriptor().Select(gd => new Gen(gd, i++)).ToList();

            int swapindex1 = (int)RandomGenerator.GetInstance().GetRandom(0, Nodes);
            int swapindex2 = (int)RandomGenerator.GetInstance().GetRandom(0, Nodes);
            gens.Swap(swapindex1, swapindex2);

            return gens;
        }
    }
}
