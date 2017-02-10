using MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Mutation;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System.Collections.Generic;
using System.Linq;
using System;
using MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Apariation;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary
{
    public class AutoEvolutionaryInformationFabrik<T> : IIndividualFabrik<AutoevolutionaryInformation> where T : IAutoEvolutionaryFitnessMatcher
    {
        IAutoEvolutionaryFitnessMatcherFabrik<T> FitnessMatcherFabrik;
        List<GenDescriptor> AutoEvolutionaryInformationGeneticDescriptor = new List<GenDescriptor> {
            new GenDescriptor("Mutation choose probability", 0, 1, 0.0001f),
            new GenDescriptor("Mutation amplitude porcentage", 0, 1, 0.0001f),
            new GenDescriptor("Apariate dominance porcentage",0, 1, 0.0001f)
        };

        public AutoEvolutionaryInformationFabrik(IAutoEvolutionaryFitnessMatcherFabrik<T> fitnessMatcherFabrik)
        {
            FitnessMatcherFabrik = fitnessMatcherFabrik;
        }

        public List<GenDescriptor> GetGeneticDescriptor()
        {
            var descriptor = AutoEvolutionaryInformationGeneticDescriptor.ToList();
            descriptor.AddRange(Enumerable.Repeat(new GenDescriptor("Mutation type prob",0, 1, 0.001f), MutationManager.GetInstance().GetMutationTypes()));
            descriptor.AddRange(Enumerable.Repeat(new GenDescriptor("Apariation type prob", 0, 1, 0.001f), MutationManager.GetInstance().GetMutationTypes()));
            descriptor.AddRange(FitnessMatcherFabrik.GetGeneticDescriptor());
            return descriptor;
        }

        public List<Gen> GetRandomGenList()
        {
            return GetGeneticDescriptor().Select(gd => new Gen(gd)).ToList();
        }

        public int GetGenCount()
        {
            return AutoEvolutionaryInformationGeneticDescriptor.Count + MutationManager.GetInstance().GetMutationTypes() + ApariationManager.GetInstance().GetApariationTypes() + FitnessMatcherFabrik.GetGenCount();
        }

        public AutoevolutionaryInformation Create(List<Gen> gens)
        {
            return new AutoevolutionaryInformation(
                FitnessMatcherFabrik.Create(gens.Skip(AutoEvolutionaryInformationGeneticDescriptor.Count + MutationManager.GetInstance().GetMutationTypes() + ApariationManager.GetInstance().GetApariationTypes()).ToList()), 
                gens[0].Value, 
                gens[1].Value, 
                gens[2].Value, 
                gens.Skip(3).Take(MutationManager.GetInstance().GetMutationTypes()).Select(o => o.Value).ToList(),
                gens.Skip(3 + MutationManager.GetInstance().GetMutationTypes()).Take(ApariationManager.GetInstance().GetApariationTypes()).Select(o => o.Value).ToList());
        }

    }
}
