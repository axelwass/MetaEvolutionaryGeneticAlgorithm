using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using MetaEvolutionaryGeneticAlgorithm.Common;

namespace ExampleApplication.Examples.TravelSalesman
{
    class TravelSalesmanIndividualFabrik : IIndividualFabrik<TravelSalesmanIndividual>
    {
        int Nodes;

        public TravelSalesmanIndividualFabrik(int nodes)
        {
            Nodes = nodes;
        }

        public TravelSalesmanIndividual Create(List<Gen> gens)
        {
            var travelOrder = new List<int>(Enumerable.Range(0, Nodes));
            int i = 0;
            foreach (var gen in gens)
            {
                travelOrder.Swap(i++, (int)gen.Value);
            }

            return new TravelSalesmanIndividual(travelOrder);
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
                gens.Add(new GenDescriptor(0, Nodes - 0.001f));
            }
            return gens;
        }
    }
}
