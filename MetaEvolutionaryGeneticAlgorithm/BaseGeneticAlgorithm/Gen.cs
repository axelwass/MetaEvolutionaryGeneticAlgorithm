
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
    }
}
