using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Apariation
{
    public interface IApariationResolver
    {
        List<Gen> Apariate(List<Gen> mate1, List<Gen> mate2, float dominance_porcentage);
    }
}
