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
            var population = new List<Genome>();
            for (int i = 0; i < parameters.MaxPopulation; i++)
            {
                var genDesc = parameters.IndividualFabrik.GetGeneticDescriptor();
                genDesc.AddRange(parameters.EvolutionaryInformationFabrik.GetGeneticDescriptor());
                population.Add(new Genome(genDesc));
            }
            return population;
        }
        
        public AutoEvolutionaryGeneticAlgorithm(AutoevolutionaryGeneticAlgorithmParameters<T,U> parameters)
            : this(parameters, GenerateInitialPopulation(parameters), 0)
        {}

        public override void AdvanceGenerations(int generations)
        {
            do
            {
                ChallengesGeneration();

                DisposeDeads();
            }
            while (Population.Count > GeneticAlgorithmInformation.MaxPopulation);


            ApariateGeneration();

            GenerationNumber++;
        }

        private void ChallengesGeneration()
        {
            var evaluationScenario = GeneticAlgorithmInformation.ScenarioGeneratior.GenerateEvaluationScenario();
            
            foreach(var genomeWarper in Population)
            {
                genomeWarper.Challange(evaluationScenario);
            }

            int aprovedNumber = (int)Math.Floor(Population.Count * GeneticAlgorithmInformation.AprovedPorcentage);
            foreach(var g in Population.OrderBy(g => g.getNormalizedFitness()).Take(aprovedNumber))
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
                var mate = g.ChooseMate(Population.OrderBy(x => RandomGenerator.GetInstance().GetRandom(0,1)).Take(GeneticAlgorithmInformation.ApariateTournamentCount).ToList());
                var childs = g.Apariate(mate).Where(child => !(child.MatchGenome(g.WarpedGenome) || child.MatchGenome(mate.WarpedGenome)));
                newGeneration.AddRange(
                    childs.Select(genome => new AutoEvolutionaryGenomeWarper<T,U>(GeneticAlgorithmInformation.IndividualFabrik, GeneticAlgorithmInformation.EvolutionaryInformationFabrik, genome, GeneticAlgorithmInformation.InitialLives))
                    );
            }
            Population.AddRange(newGeneration);
        }
    }
}
