using System.Collections.Generic;
using System.Linq;

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
            return new Genome(Descriptors, Gens.Select(g => g.Mutate(type, choose_probability, amplitude_porcentage)).ToList());
        }

        public Genome Apariate(int type, Genome other, float dominance_porcentage)
        {
            return new Genome(Descriptors, Gens.Zip(other.Gens, (g1, g2) => g1.Apariate(type, g2, dominance_porcentage)).ToList());
        }

        public List<Gen> GetGens()
        {
            return Gens;
        }

    }

}
