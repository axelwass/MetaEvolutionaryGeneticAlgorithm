using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using ExampleApplication.Examples.TravelSalesman;

namespace ExapleApplication.Examples.TravelSalesman
{
    class TravelSalesmanSortIndividualFabrik : IIndividualFabrik<TravelSalesmanIndividual>
    {
        int Nodes;

        public TravelSalesmanSortIndividualFabrik(int nodes)
        {
            Nodes = nodes;
        }

        public TravelSalesmanIndividual Create(List<Gen> gens)
        {
            int i = 0;
            var pairs = new List<Tuple<int,float>>();
            foreach (var gen in gens)
            {
                pairs.Add(new Tuple<int, float>(i++, gen.Value));
            }
            var travelOrder = pairs.OrderBy(o => o.Item2).Select(o => o.Item1).ToList();
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
                gens.Add(new GenDescriptor("Node sort order field",0, 1, 0.0001f));
            }
            return gens;
        }


        public Genome GetRandomGenome()
        {
            return new Genome(GetGeneticDescriptor());
        }
    }
}
