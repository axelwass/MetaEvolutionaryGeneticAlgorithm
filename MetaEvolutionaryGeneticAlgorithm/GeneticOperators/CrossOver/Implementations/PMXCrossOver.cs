using MetaEvolutionaryGeneticAlgorithm.GeneticOperators.CrossOver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using MetaEvolutionaryGeneticAlgorithm.Common;

namespace AlgoritmoGeneticoAutoevolutivo.GeneticOperators.CrossOver.Implementations
{
    public class PMXCrossOver : ICrossOverResolver
    {
        public List<Gen> Cross(List<Gen> mate1, List<Gen> mate2, float dominance_porcentage)
        {
            int count = (int)(mate1.Count * RandomGenerator.GetInstance().GetRandom(dominance_porcentage/2, dominance_porcentage));

            var pathMate1 = mate1.Skip((int)RandomGenerator.GetInstance().GetRandom(0, mate1.Count - count)).Take(count);

            var mate2Other = mate2.Where(g1 => pathMate1.Where(g2 => g2.Value == g1.Value).Count() == 0).ToList();

            var position = (int)RandomGenerator.GetInstance().GetRandom(0, mate2Other.Count);

            var child = mate2Other.Take(position).ToList();

            child.AddRange(pathMate1);

            child.AddRange(mate2Other.Skip(position));

            return child.Select(g => new Gen(g.Descriptor,g.Value)).ToList();
        }
    }
}
