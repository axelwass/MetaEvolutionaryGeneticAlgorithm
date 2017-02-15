using MetaEvolutionaryGeneticAlgorithm.GeneticOperators.CrossOver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;

namespace AlgoritmoGeneticoAutoevolutivo.GeneticOperators.CrossOver.Implementations
{
    public class DividedCrossOver : ICrossOverResolver
    {
        int Cant;
        ICrossOverResolver Resolver1;
        ICrossOverResolver Resolver2;

        public DividedCrossOver(int cant, ICrossOverResolver resolver1, ICrossOverResolver resolver2)
        {
            Cant = cant;
            Resolver1 = resolver1;
            Resolver2 = resolver2;
        }

        public List<Gen> Cross(List<Gen> mate1, List<Gen> mate2, float dominance_porcentage)
        {
            var child = Resolver1.Cross(mate1.Take(Cant).ToList(), mate2.Take(Cant).ToList(), dominance_porcentage);

            child.AddRange(Resolver2.Cross(mate1.Skip(Cant).ToList(), mate2.Skip(Cant).ToList(), dominance_porcentage));

            return child;
        }
    }
}
