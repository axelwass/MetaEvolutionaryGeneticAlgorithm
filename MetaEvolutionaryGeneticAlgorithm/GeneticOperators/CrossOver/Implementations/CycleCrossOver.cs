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
    public class CycleCrossOver : ICrossOverResolver
    {
        public List<Gen> Cross(List<Gen> mate1, List<Gen> mate2, float dominance_porcentage)
        {
            List<Gen> child = Enumerable.Repeat<Gen>(null, mate1.Count).ToList();

            int cycles = 0;
            int mate1Cycles = 0;

            for (int i = 0; i < mate1.Count; i++)
            {
                if(child[i] == null)
                {
                    float prov = cycles == 0 ? 0.5f :
                                mate1Cycles == 0 ? 0.8f :
                                mate1Cycles == cycles ? 0.2f :
                                0.5f;
                                 
                    if (RandomGenerator.GetInstance().GetRandom(0, 1) < prov)
                    {
                        mate1Cycles++;
                        completeCycle(child, mate1, mate2, i);
                    }
                    else
                    {
                        completeCycle(child, mate2, mate1, i);
                    }
                    cycles++;

                }
            }

            return child;
        }

        private void completeCycle(List<Gen> child, List<Gen> mate1, List<Gen> mate2, int i)
        {
            while (child[i] == null)
            {
                child[i] = new Gen(mate1[i].Descriptor, mate1[i].Value);
                i = mate2.FindIndex(g => g.Value == child[i].Value);
            }
        }
    }
}
