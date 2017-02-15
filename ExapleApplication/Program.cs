using ExampleApplication.Examples.TravelSalesman;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMatchers.DefaultImplementation;
using MetaEvolutionaryGeneticAlgorithm.Common;
using MoreLinq;
using System;
using System.Linq;
using System.Threading;
using ExapleApplication.Examples.TravelSalesman;
using MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Mutation;
using MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Mutation.Implementations;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using MetaEvolutionaryGeneticAlgorithm.GeneticOperators.CrossOver;
using AlgoritmoGeneticoAutoevolutivo.GeneticOperators.CrossOver.Implementations;
using AlgoritmoGeneticoAutoevolutivo.GeneticOperators.Mutation.Implementations;

namespace ExapleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] fileContet;
            using (WebClient client = new WebClient())
            {
                fileContet = client.DownloadData("http://people.sc.fsu.edu/~jburkardt/datasets/tsp/att48_d.txt");
            }
            String input = Encoding.UTF8.GetString(fileContet).Trim();

            int i = 0, j = 0;
            string[] rows = input.Split('\n');
            float [,] distancesVector = new float[rows.Length, rows.Length];
            float maxPathLength = 0;
            foreach (var row in rows)
            {
                j = 0;
                foreach (var col in Regex.Split(row.Trim(),@"\s+"))
                {
                    distancesVector[i, j] = float.Parse(col.Trim());
                    if (distancesVector[i, j] > maxPathLength)
                    {
                        maxPathLength = distancesVector[i, j];
                    }
                    j++;
                }
                i++;
            }
            int nodes = rows.Length;

            //MutationManager.GetInstance().Register(new ClassicMutationResolver());
            //MutationManager.GetInstance().Register(new SwapMutationResolver());

            MutationManager.GetInstance().Register(new DividedMutation(nodes, new SwapMutationResolver(), new ClassicMutationResolver()));

            CrossOverManager.GetInstance().Register(new DividedCrossOver(nodes, new PMXCrossOver(), new ClassicCrossOverResolver()));

            var fitnessMatcherFabrik = new AutoevolutionaryFitnessMatcherDefaultImplementationFabrik();

            var GAInfo = new AutoevolutionaryGeneticAlgorithmParameters<TravelSalesmanIndividual, AutoevolutionaryFitnessMatcherDefaultImplementation>
            {
                IndividualFabrik = new TravelSalesmanPathIndividualFabrik(nodes),
                ScenarioGeneratior = new TravelSalesmanScenarioGenerator(distancesVector, nodes, maxPathLength),
                EvolutionaryInformationFabrik = new AutoEvolutionaryInformationFabrik<AutoevolutionaryFitnessMatcherDefaultImplementation>(fitnessMatcherFabrik),
                MaxPopulation = 100,
                InitialLives = 3,
                AprovedPorcentage = 0.5f,
                ApariateTournamentCount = 5,
                DeathTournamentCount = 6,
                ForeingersByGeneration = 10
            };

            var GA = new AutoEvolutionaryGeneticAlgorithm<TravelSalesmanIndividual, AutoevolutionaryFitnessMatcherDefaultImplementation>(GAInfo);

            //Console.Out.WriteLine(Arrays.PrettyPrintMatrix<float>(scenarioGenerator.DistancesVector));

            for (int g = 0; g < 5000; g++)
            {
                GA.AdvanceGenerations(1);
                var bestIndividual = GA.Population.MaxBy(o => o.getNormalizedFitness());
                Console.Out.WriteLine("Generation: " + GA.GenerationNumber);
                Console.Out.WriteLine("AVG apariate dominance porcentage: " + GA.Population.Average(o => o.EvolutionInformtaion.ApariateGenDominancePorcentage));
                Console.Out.WriteLine("Best apariate dominance porcentage: " + bestIndividual.EvolutionInformtaion.ApariateGenDominancePorcentage);
                Console.Out.WriteLine("AVG mutate amplitude: " + GA.Population.Average(o => o.EvolutionInformtaion.MutateGenAmplitudePorcentage));
                Console.Out.WriteLine("Best mutate amplitude: " + bestIndividual.EvolutionInformtaion.MutateGenAmplitudePorcentage);
                Console.Out.WriteLine("AVG mutate choose prob: " + GA.Population.Average(o => o.EvolutionInformtaion.MutateGenChooseProbability));
                Console.Out.WriteLine("Best mutate choose prob: " + bestIndividual.EvolutionInformtaion.MutateGenChooseProbability);
                Console.Out.WriteLine("Best mutation type prob: " + bestIndividual.EvolutionInformtaion.MutationTypeProbability[0]);
                Console.Out.WriteLine("Actual Population: " + GA.Population.Count);
                Console.Out.WriteLine("Best path: " + String.Join(",", bestIndividual.Individual.travelOrder));
                Console.Out.WriteLine("Best fitness: " + bestIndividual.getNormalizedFitness());
                TravelSalesmanFitness fitness = (TravelSalesmanFitness)(bestIndividual.Fitness);
                Console.Out.WriteLine("Best fitness: " + fitness.TotalDistance);
            }

            while(true) Thread.Sleep(1000);
        }
    }
}
