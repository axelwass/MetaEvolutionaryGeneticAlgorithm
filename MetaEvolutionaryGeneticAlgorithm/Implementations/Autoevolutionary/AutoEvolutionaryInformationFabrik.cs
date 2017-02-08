﻿using AlgoritmoGeneticoAutoevolutivo.GeneticOperators.Mutation;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System.Collections.Generic;
using System.Linq;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary
{
    public class AutoEvolutionaryInformationFabrik<T> : IIndividualFabrik<AutoevolutionaryInformation> where T : IAutoEvolutionaryFitnessMatcher
    {
        IAutoEvolutionaryFitnessMatcherFabrik<T> FitnessMatcherFabrik;
        List<GenDescriptor> AutoEvolutionaryInformationGeneticDescriptor = new List<GenDescriptor> {
            new GenDescriptor("Mutation choose probability", 0, 1),
            new GenDescriptor("Mutation amplitude porcentage", 0, 1),
            new GenDescriptor("Apariate dominance porcentage",0, 1)
        };

        public AutoEvolutionaryInformationFabrik(IAutoEvolutionaryFitnessMatcherFabrik<T> fitnessMatcherFabrik)
        {
            FitnessMatcherFabrik = fitnessMatcherFabrik;
        }

        public List<GenDescriptor> GetGeneticDescriptor()
        {
            var descriptor = AutoEvolutionaryInformationGeneticDescriptor.ToList();
            descriptor.AddRange(Enumerable.Repeat(new GenDescriptor("Mutation type prob",0, 1), MutationManager.GetInstance().GetMutationTypes()));
            descriptor.AddRange(FitnessMatcherFabrik.GetGeneticDescriptor());
            return descriptor;
        }

        public int GetGenCount()
        {
            return AutoEvolutionaryInformationGeneticDescriptor.Count + MutationManager.GetInstance().GetMutationTypes() + FitnessMatcherFabrik.GetGenCount();
        }

        public AutoevolutionaryInformation Create(List<Gen> gens)
        {
            return new AutoevolutionaryInformation(
                FitnessMatcherFabrik.Create(gens.Skip(AutoEvolutionaryInformationGeneticDescriptor.Count + MutationManager.GetInstance().GetMutationTypes()).ToList()), 
                gens[0].Value, 
                gens[1].Value, 
                gens[2].Value, 
                gens.Skip(3).Take(MutationManager.GetInstance().GetMutationTypes()).Select(o => o.Value).ToList());
        }
    }
}
