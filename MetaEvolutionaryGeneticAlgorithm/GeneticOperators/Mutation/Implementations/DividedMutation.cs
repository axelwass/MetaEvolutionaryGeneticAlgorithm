using MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Mutation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;

namespace AlgoritmoGeneticoAutoevolutivo.GeneticOperators.Mutation.Implementations
{
    public class DividedMutation : IMutationResolver
    {

        int Cant;
        IMutationResolver Resolver1;
        IMutationResolver Resolver2;

        public DividedMutation(int cant, IMutationResolver resolver1, IMutationResolver resolver2)
        {
            Cant = cant;
            Resolver1 = resolver1;
            Resolver2 = resolver2;
        }
        public List<Gen> Mutate(List<Gen> gens, float choose_probability, float amplitude_porcentage)
        {

            var mutated = Resolver1.Mutate(gens.Take(Cant).ToList(), choose_probability, amplitude_porcentage);

            mutated.AddRange(Resolver2.Mutate(gens.Skip(Cant).ToList(), choose_probability, amplitude_porcentage));

            return mutated;
        }
    }
}
