using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.GA;
using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.Problem;

namespace AlgoritmoGeneticoAutoevolutivo.Implementations.Autoevolutionary
{
    class AutoEvolutionaryGeneticAlgorithm<T>: AGeneticAlgorithm<T, AutoEvolutionaryGenomeWarper<T>>
    {
        private readonly int InitialLives;
        private readonly int ChallengesByGeneration;
        private readonly float AprovedPorcentage;


        AutoEvolutionaryInformationFabrik EvolutionaryInformationFabrik;
        
        public AutoEvolutionaryGeneticAlgorithm(IIndividualFabrik<T> individualFabrik, AutoEvolutionaryInformationFabrik evolutionaryInformationFabrik, IEvaluationScenarioGenerator<T> scenarioGenerator, List<Genome> generation, int generationNumber,
             int initialLives, int challengesByGeneration, float aprovedPorcentage)
            : base( individualFabrik, scenarioGenerator)
        {
            EvolutionaryInformationFabrik = evolutionaryInformationFabrik;
            InitialLives = initialLives;
            ChallengesByGeneration = challengesByGeneration;
            AprovedPorcentage = aprovedPorcentage;
            Generation = generation.Select(g => new AutoEvolutionaryGenomeWarper<T>(individualFabrik, evolutionaryInformationFabrik, g, initialLives)).ToList();
            GenerationNumber = generationNumber;
        }

        static private List<Genome> GenerateInitialPopulation(IIndividualFabrik<T> individualFabrik, AutoEvolutionaryInformationFabrik evolutionaryInformationFabrik, int initialPopulation)
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
        
        public AutoEvolutionaryGeneticAlgorithm(IIndividualFabrik<T> individualFabrik, AutoEvolutionaryInformationFabrik evolutionaryInformationFabrik,IEvaluationScenarioGenerator<T> scenarioGenerator,
             int initialLives, int challengesByGeneration, float aprovedPorcentage, int initialPopulation)
            : this(individualFabrik, evolutionaryInformationFabrik, scenarioGenerator, GenerateInitialPopulation(individualFabrik, evolutionaryInformationFabrik, initialPopulation), 0, initialLives, challengesByGeneration, aprovedPorcentage)
        {}

        public override void AdvanceGenerations(int generations)
        {
            for (int i = 0; i < ChallengesByGeneration; i++)
            {
                ChallengesGeneration();

                DisposeDeads();
            }
            
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
            var rnd = new Random();
            foreach(var g in Generation)
            {
                var mate = g.ChooseMate(Generation.OrderBy(x => rnd.Next()).Take(5).ToList());
                Generation.AddRange(
                    g.Apariate(mate).Select(genome => new AutoEvolutionaryGenomeWarper<T>(IndividualFabrik,EvolutionaryInformationFabrik, genome, InitialLives))
                    );
            }
        }
    }
}
