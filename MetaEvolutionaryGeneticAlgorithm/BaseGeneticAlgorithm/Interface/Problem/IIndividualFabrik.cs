using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.Problem
{
    interface IIndividualFabrik<T>
    {

        List<GenDescriptor> GetGeneticDescriptor();
        int GetGenCount();
        T Create(List<Gen> gens);
    }
}
