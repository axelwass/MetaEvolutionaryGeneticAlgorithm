
namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm
{
    public class Gen
    {
        public float Value { get; private set; }
        GenDescriptor Descriptor;

        public Gen(GenDescriptor desc, float value)
        {
            Descriptor = desc;
            Value = Descriptor.GetRandom();
        }

        public Gen(GenDescriptor desc) : this(desc, desc.GetRandom()){}

        public Gen Mutate(int type, float chooseProbability, float amplitudePorcentage)
        {
            return new Gen(Descriptor,Descriptor.Mutate(type, Value, chooseProbability, amplitudePorcentage));
        }

        public Gen Apariate(int type, Gen other, float dominancePorcentage)
        {
            return new Gen(Descriptor, Descriptor.Apariate(type, Value, other.Value, dominancePorcentage));
        }
    }
}
