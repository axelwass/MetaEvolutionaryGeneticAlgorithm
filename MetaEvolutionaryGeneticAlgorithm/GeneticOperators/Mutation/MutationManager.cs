using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Mutation
{
    public class MutationManager
    {
        static MutationManager Instance = new MutationManager();
        List<IMutationResolver> Resolvers = new List<IMutationResolver>();

        public static MutationManager GetInstance()
        {
            return Instance;
        }

        public void Register(IMutationResolver mr)
        {
            Resolvers.Add(mr);
        }

        public List<Gen> Mutate(int type, List<Gen> gens, float choose_probability, float amplitude_porcentage)
        {
            return Resolvers[type].Mutate(gens, choose_probability, amplitude_porcentage);
        }

        public int GetMutationTypes()
        {
            return Resolvers.Count;
        }

    }
}
