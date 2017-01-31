using ExampleApplication.Examples.TravelSalesman;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMatchers.DefaultImplementation;
using AlgoritmoGeneticoAutoevolutivo.Common;
using MoreLinq;
using System;
using System.Threading;

namespace ExapleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int nodes = 100;
            float maxDistance = 10.0f;

            var individualFabrik = new TravelSalesmanIndividualFabrik(nodes);
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
                Console.Out.WriteLine("Actual Population: " + GA.Generation.Count);
                Console.Out.WriteLine("Best path: " + String.Join(",", bestIndividual.Individual.travelOrder));
                Console.Out.WriteLine("Best fitness: " + bestIndividual.getNormalizedFitness());
            }

            while(true) Thread.Sleep(1000);
        }
    }
}
