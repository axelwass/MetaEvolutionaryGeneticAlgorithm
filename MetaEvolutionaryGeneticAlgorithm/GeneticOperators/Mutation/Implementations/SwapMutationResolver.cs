using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using MetaEvolutionaryGeneticAlgorithm.Common;

namespace MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Mutation.Implementations
{
    public class SwapMutationResolver : IMutationResolver
    {
        public List<Gen> Mutate(List<Gen> gens, float chooseProbability, float amplitudePorcentage)
        {
            List<Gen> genCopy = gens.Select(g => new Gen(g.Descriptor, g.Value)).ToList();
            int numberGensToSwap = (int)(gens.Count * chooseProbability);

            for(int i=0;i< numberGensToSwap; i++)
            {
                int swapindex1 = (int)RandomGenerator.GetInstance().GetRandom(0, genCopy.Count);
                int swapindex2 = (int)RandomGenerator.GetInstance().GetRandom(0, genCopy.Count);
                if (genCopy[swapindex1].Descriptor.Desc.Equals(genCopy[swapindex2].Descriptor.Desc))
                {
                    genCopy.Swap(swapindex1, swapindex2);
                }
            }
            return genCopy;
        }
    }
}
