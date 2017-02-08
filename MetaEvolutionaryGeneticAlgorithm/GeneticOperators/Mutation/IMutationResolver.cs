using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoGeneticoAutoevolutivo.GeneticOperators.Mutation
{
    public interface IMutationResolver
    {
        List<Gen> Mutate(List<Gen> Gens, float choose_probability, float amplitude_porcentage);

    }
}
