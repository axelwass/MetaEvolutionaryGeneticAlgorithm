using ExampleApplication.Examples.TravelSalesman;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMatchers.DefaultImplementation;
using AlgoritmoGeneticoAutoevolutivo.Common;
using MoreLinq;
using System;
using System.Linq;
using System.Threading;
using ExapleApplication.Examples.TravelSalesman;
using AlgoritmoGeneticoAutoevolutivo.GeneticOperators.Mutation;
using AlgoritmoGeneticoAutoevolutivo.GeneticOperators.Mutation.Implementations;

namespace ExapleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int nodes = 100;
            float maxDistance = 10.0f;

            MutationManager.GetInstance().Register(new ClassicMutationResolver());
            MutationManager.GetInstance().Register(new SwapMutationResolver());

            var individualFabrik = new TravelSalesmanSortIndividualFabrik(nodes);
            var fitnessMatcherFabrik = new AutoevolutionaryFitnessMatcherDefaultImplementationFabrik();
            var informationFabrik = new AutoEvolutionaryInformationFabrik<AutoevolutionaryFitnessMatcherDefaultImplementation>(fitnessMatcherFabrik);
            var scenarioGenerator = new TravelSalesmanScenarioGenerator(nodes, maxDistance);

            var GA = new AutoEvolutionaryGeneticAlgorithm<TravelSalesmanIndividual, AutoevolutionaryFitnessMatcherDefaultImplementation>(individualFabrik, informationFabrik, scenarioGenerator, 2, 0.5f, 10, 50);

            Console.Out.WriteLine(Arrays.PrettyPrintMatrix<float>(scenarioGenerator.DistancesVector));

            for (int i = 0; i < 100; i++)
            {
                GA.AdvanceGenerations(1);
                var bestIndividual = GA.Generation.MaxBy(o => o.getNormalizedFitness());
                Console.Out.WriteLine("Generation: " + GA.GenerationNumber);
                Console.Out.WriteLine("AVG apariate dominance porcentage: " + GA.Generation.Average(o => o.EvolutionInformtaion.ApariateGenDominancePorcentage));
                Console.Out.WriteLine("Best apariate dominance porcentage: " + bestIndividual.EvolutionInformtaion.ApariateGenDominancePorcentage);
                Console.Out.WriteLine("AVG mutate amplitude: " + GA.Generation.Average(o => o.EvolutionInformtaion.MutateGenAmplitudePorcentage));
                Console.Out.WriteLine("Best mutate amplitude: " + bestIndividual.EvolutionInformtaion.MutateGenAmplitudePorcentage);
                Console.Out.WriteLine("AVG mutate choose prob: " + GA.Generation.Average(o => o.EvolutionInformtaion.MutateGenChooseProbability));
                Console.Out.WriteLine("Best mutate choose prob: " + bestIndividual.EvolutionInformtaion.MutateGenChooseProbability);
                Console.Out.WriteLine("Best mutation type prob: " + bestIndividual.EvolutionInformtaion.TypeProbability[0] + ", " + bestIndividual.EvolutionInformtaion.TypeProbability[1]);
                Console.Out.WriteLine("Actual Population: " + GA.Generation.Count);
                Console.Out.WriteLine("Best path: " + String.Join(",", bestIndividual.Individual.travelOrder));
                Console.Out.WriteLine("Best fitness: " + bestIndividual.getNormalizedFitness());
            }

            while(true) Thread.Sleep(1000);
        }
    }
}
