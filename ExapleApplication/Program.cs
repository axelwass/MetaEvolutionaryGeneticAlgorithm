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
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ExapleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] fileContet;
            using (WebClient client = new WebClient())
            {
                fileContet = client.DownloadData("http://people.sc.fsu.edu/~jburkardt/datasets/tsp/dantzig42_d.txt");
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

            MutationManager.GetInstance().Register(new ClassicMutationResolver());
            MutationManager.GetInstance().Register(new SwapMutationResolver());

            var individualFabrik = new TravelSalesmanSortIndividualFabrik(nodes);
            var fitnessMatcherFabrik = new AutoevolutionaryFitnessMatcherDefaultImplementationFabrik();
            var informationFabrik = new AutoEvolutionaryInformationFabrik<AutoevolutionaryFitnessMatcherDefaultImplementation>(fitnessMatcherFabrik);
            var scenarioGenerator = new TravelSalesmanScenarioGenerator(distancesVector,nodes,maxPathLength);

            var GA = new AutoEvolutionaryGeneticAlgorithm<TravelSalesmanIndividual, AutoevolutionaryFitnessMatcherDefaultImplementation>(individualFabrik, informationFabrik, scenarioGenerator, 2, 0.5f, 10, 100);

            //Console.Out.WriteLine(Arrays.PrettyPrintMatrix<float>(scenarioGenerator.DistancesVector));

            for (int g = 0; g < 1000; g++)
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
                TravelSalesmanFitness fitness = (TravelSalesmanFitness)(bestIndividual.Fitness);
                Console.Out.WriteLine("Best fitness: " + fitness.TotalDistance);
            }

            while(true) Thread.Sleep(1000);
        }
    }
}
