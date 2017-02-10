using MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Mutation;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm
{
    public class Genome
    {
        List<Gen> Gens;
        List<GenDescriptor> Descriptors;

        public Genome(List<GenDescriptor> descs, List<Gen> gens)
        {
            Descriptors = descs;
            Gens = gens;
        }

        public Genome(List<GenDescriptor> descs) : this(descs, descs.Select(gd => new Gen(gd)).ToList()) { }

        public Genome Mutate(int type, float choose_probability, float amplitude_porcentage)
        {
            return new Genome(Descriptors, MutationManager.GetInstance().Mutate(type, Gens, choose_probability, amplitude_porcentage));
        }

        public Genome Apariate(int type, Genome other, float dominance_porcentage)
        {
            return new Genome(Descriptors, Gens.Zip(other.Gens, (g1, g2) => g1.Apariate(type, g2, dominance_porcentage)).ToList());
        }

        public List<Gen> GetGens()
        {
            return Gens;
        }

        internal bool MatchGenome(Genome other)
        {
            for(int i = 0; i < Gens.Count; i++)
            {
                if(Gens[i].Value - other.Gens[i].Value > Descriptors[i].Epsilon)
                {
                    return false;
                }
            }
            return true;
        }
    }

}
