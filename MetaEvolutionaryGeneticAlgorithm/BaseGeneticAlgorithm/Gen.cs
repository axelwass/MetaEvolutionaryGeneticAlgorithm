
namespace MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm
{
    public class Gen
    {
        public float Value { get; private set; }
        public GenDescriptor Descriptor;

        public Gen(GenDescriptor desc, float value)
        {
            Descriptor = desc;
            Value = value;
        }

        public Gen(GenDescriptor desc) : this(desc, desc.GetRandom()){}

        public Gen Apariate(int type, Gen other, float dominancePorcentage)
        {
            return new Gen(Descriptor, Descriptor.Apariate(type, Value, other.Value, dominancePorcentage));
        }
    }
}
