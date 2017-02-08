using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.GA;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using AlgoritmoGeneticoAutoevolutivo.Common;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary
{
    public class AutoEvolutionaryGeneticAlgorithm<T, U>: AGeneticAlgorithm<T, AutoEvolutionaryGenomeWarper<T,U>> where U : IAutoEvolutionaryFitnessMatcher
    {
        private readonly int InitialLives;
        private readonly int MaxPopulation;
        private readonly float AprovedPorcentage;


        AutoEvolutionaryInformationFabrik<U> EvolutionaryInformationFabrik;
        
        public AutoEvolutionaryGeneticAlgorithm(IIndividualFabrik<T> individualFabrik, AutoEvolutionaryInformationFabrik<U> evolutionaryInformationFabrik, IEvaluationScenarioGenerator<T> scenarioGenerator, List<Genome> generation, int generationNumber,
             int initialLives, float aprovedPorcentage, int maxPopulation)
            : base( individualFabrik, scenarioGenerator)
        {
            EvolutionaryInformationFabrik = evolutionaryInformationFabrik;
            InitialLives = initialLives;
            MaxPopulation = maxPopulation;
            AprovedPorcentage = aprovedPorcentage;
            Generation = generation.Select(g => new AutoEvolutionaryGenomeWarper<T,U>(individualFabrik, evolutionaryInformationFabrik, g, initialLives)).ToList();
            GenerationNumber = generationNumber;
        }

        static private List<Genome> GenerateInitialPopulation(IIndividualFabrik<T> individualFabrik, AutoEvolutionaryInformationFabrik<U> evolutionaryInformationFabrik, int initialPopulation)
        {
            var population = new List<Genome>();
            for (int i = 0; i < initialPopulation; i++)
            {
                var genDesc = individualFabrik.GetGeneticDescriptor();
                genDesc.AddRange(evolutionaryInformationFabrik.GetGeneticDescriptor());
                population.Add(new Genome(genDesc));
            }
            return population;
        }
        
        public AutoEvolutionaryGeneticAlgorithm(IIndividualFabrik<T> individualFabrik, AutoEvolutionaryInformationFabrik<U> evolutionaryInformationFabrik,IEvaluationScenarioGenerator<T> scenarioGenerator,
             int initialLives, float aprovedPorcentage, int initialPopulation, int maxPopulation)
            : this(individualFabrik, evolutionaryInformationFabrik, scenarioGenerator, GenerateInitialPopulation(individualFabrik, evolutionaryInformationFabrik, initialPopulation), 0, initialLives, aprovedPorcentage, maxPopulation)
        {}

        public override void AdvanceGenerations(int generations)
        {
            do
            {
                ChallengesGeneration();

                DisposeDeads();
            }
            while (Generation.Count > MaxPopulation);


            ApariateGeneration();

            GenerationNumber++;
        }

        private void ChallengesGeneration()
        {
            var evaluationScenario = ScenarioGeneratior.GenerateEvaluationScenario();
            
            foreach(var genomeWarper in Generation)
            {
                genomeWarper.Challange(evaluationScenario);
            }

            int aprovedNumber = (int)Math.Floor(Generation.Count * AprovedPorcentage);
            foreach(var g in Generation.OrderBy(g => g.getNormalizedFitness()).Take(aprovedNumber))
            {
                g.looseLive();
            }
        }

        private void DisposeDeads()
        {
            Generation = Generation.Where(gw => !gw.isDead()).ToList();
        }

        private void ApariateGeneration()
        {
            var newGeneration = new List<AutoEvolutionaryGenomeWarper<T, U>>();
            foreach (var g in Generation)
            {
                var mate = g.ChooseMate(Generation.OrderBy(x => RandomGenerator.GetInstance().GetRandom(0,1)).Take(5).ToList());
                newGeneration.AddRange(
                    g.Apariate(mate).Select(genome => new AutoEvolutionaryGenomeWarper<T,U>(IndividualFabrik,EvolutionaryInformationFabrik, genome, InitialLives))
                    );
            }
            Generation.AddRange(newGeneration);
        }
    }
}
