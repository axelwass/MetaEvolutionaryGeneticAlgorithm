using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem
{
    public interface IIndividualFabrik<T>
    {

        List<GenDescriptor> GetGeneticDescriptor();
        List<Gen> GetRandomGenList();
        int GetGenCount();
        T Create(List<Gen> gens);
    }
}
