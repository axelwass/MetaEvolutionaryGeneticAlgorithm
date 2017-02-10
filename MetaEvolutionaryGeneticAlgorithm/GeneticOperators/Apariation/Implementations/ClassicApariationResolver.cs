using MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Apariation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using MetaEvolutionaryGeneticAlgorithm.Common;

namespace AlgoritmoGeneticoAutoevolutivo.GeneticOperators.Apariation.Implementations
{
    public class ClassicApariationResolver : IApariationResolver
    {
        public List<Gen> Apariate(List<Gen> mate1, List<Gen> mate2, float dominance_porcentage)
        {
            return mate1.Zip(mate2, (g1, g2) => ApariateGen(g1, g2, dominance_porcentage)).ToList();
        }

        private Gen ApariateGen(Gen g1, Gen g2, float dominance_porcentage)
        {
            return new Gen(g1.Descriptor, RandomGenerator.GetInstance().GetRandom(0, 1) < dominance_porcentage ? g1.Value : g2.Value);
        }
    }
}
