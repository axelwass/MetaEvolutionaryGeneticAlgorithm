using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.GA;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using MetaEvolutionaryGeneticAlgorithm.Common;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary
{
    public class AutoEvolutionaryGeneticAlgorithm<T, U>: AGeneticAlgorithm<T, AutoevolutionaryGeneticAlgorithmParameters<T,U>, AutoEvolutionaryGenomeWarper<T,U>> where U : IAutoEvolutionaryFitnessMatcher
    {
        
        public AutoEvolutionaryGeneticAlgorithm(AutoevolutionaryGeneticAlgorithmParameters<T, U> parameters, List<Genome> generation, int generationNumber)
            : base(parameters)
        {
            Population = generation.Select(g => new AutoEvolutionaryGenomeWarper<T,U>(parameters.IndividualFabrik, parameters.EvolutionaryInformationFabrik, g, parameters.InitialLives)).ToList();
            GenerationNumber = generationNumber;
        }

        static private List<Genome> GenerateInitialPopulation(AutoevolutionaryGeneticAlgorithmParameters<T, U> parameters)
        {
            return GeneratePopulation(parameters, parameters.MaxPopulation);
        }

        static private List<Genome> GeneratePopulation(AutoevolutionaryGeneticAlgorithmParameters<T, U> parameters, int cant)
        {
            var population = new List<Genome>();
            for (int i = 0; i < parameters.MaxPopulation; i++)
            {
                var genDesc = parameters.IndividualFabrik.GetGeneticDescriptor();
                genDesc.AddRange(parameters.EvolutionaryInformationFabrik.GetGeneticDescriptor());

                var gens = parameters.IndividualFabrik.GetRandomGenList();
                gens.AddRange(parameters.EvolutionaryInformationFabrik.GetRandomGenList());

                population.Add(new Genome(genDesc,gens));
            }
            return population;
        }
        
        public AutoEvolutionaryGeneticAlgorithm(AutoevolutionaryGeneticAlgorithmParameters<T,U> parameters)
            : this(parameters, GenerateInitialPopulation(parameters), 0)
        {}

        public override void AdvanceGenerations(int generations)
        {
            while (Population.Count > GeneticAlgorithmInformation.MaxPopulation)
            {
                Tournament();

                DisposeDeads();
            }


            ApariateGeneration();

            AddForeingers();

            GenerationNumber++;
        }

        private void AddForeingers()
        {
            var foreingersGenome = GeneratePopulation(GeneticAlgorithmInformation, GeneticAlgorithmInformation.ForeingersByGeneration);
            var foreingers = foreingersGenome.Select(g => new AutoEvolutionaryGenomeWarper<T, U>(GeneticAlgorithmInformation.IndividualFabrik, GeneticAlgorithmInformation.EvolutionaryInformationFabrik, g, GeneticAlgorithmInformation.InitialLives)).ToList();

            Population.AddRange(foreingers);
        }

        private void Tournament()
        {
            var evaluationScenario = GeneticAlgorithmInformation.ScenarioGeneratior.GenerateEvaluationScenario();

            var tournament = Population.OrderBy(x => RandomGenerator.GetInstance().GetRandom(0, 1)).Take(GeneticAlgorithmInformation.DeathTournamentCount).ToList();
            foreach (var competitor in tournament)
            {
                competitor.Challange(evaluationScenario);
            }

            int aprovedNumber = (int)Math.Floor(tournament.Count * GeneticAlgorithmInformation.AprovedPorcentage);
            foreach(var g in tournament.OrderBy(g => 1 - g.getNormalizedFitness()).Skip(aprovedNumber))
            {
                g.looseLive();
            }
        }

        private void DisposeDeads()
        {
            Population = Population.Where(gw => !gw.isDead()).ToList();
        }

        private void ApariateGeneration()
        {
            var newGeneration = new List<AutoEvolutionaryGenomeWarper<T, U>>();
            foreach (var g in Population)
            {
                var evaluationScenario = GeneticAlgorithmInformation.ScenarioGeneratior.GenerateEvaluationScenario();
                var posibleMates = Population.OrderBy(x => RandomGenerator.GetInstance().GetRandom(0, 1)).Take(GeneticAlgorithmInformation.ApariateTournamentCount).ToList();
                foreach(var mate in posibleMates)
                {
                    mate.Challange(evaluationScenario);
                }
                var chosenmMate = g.ChooseMate(posibleMates);
                var childs = g.Apariate(chosenmMate).Where(child => !(child.MatchGenome(g.WarpedGenome) || child.MatchGenome(chosenmMate.WarpedGenome)));
                newGeneration.AddRange(
                    childs.Select(genome => new AutoEvolutionaryGenomeWarper<T,U>(GeneticAlgorithmInformation.IndividualFabrik, GeneticAlgorithmInformation.EvolutionaryInformationFabrik, genome, GeneticAlgorithmInformation.InitialLives))
                    );
            }
            Population.AddRange(newGeneration);
        }
    }
}
