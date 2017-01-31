using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.GA;
using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.Problem;

namespace AlgoritmoGeneticoAutoevolutivo.Implementations.Autoevolutionary
{
    class AutoEvolutionaryGenomeWarper<T> : IGenomeWarper<T, AutoEvolutionaryGenomeWarper<T>>
    {
        int Lives;
        IFitness Fitness;
        Genome WarpedGenome;
        T Individual;
        AutoevolutionaryInformation EvolutionInformtaion;

        public AutoEvolutionaryGenomeWarper(IIndividualFabrik<T> individualFabrik, AutoEvolutionaryInformationFabrik autoEvolutionaryInformationFabrik, Genome genome, int initialLives)
        {
            WarpedGenome = genome;
            Individual = individualFabrik.Create(WarpedGenome.GetGens().Take(individualFabrik.GetGenCount()).ToList());
            EvolutionInformtaion = autoEvolutionaryInformationFabrik.Create(WarpedGenome.GetGens().Skip(individualFabrik.GetGenCount()).ToList());
            Lives = initialLives;
        }

        public bool isDead()
        {
            return Lives <= 0;
        }

        public void looseLive()
        {
            Lives--;
        }
        
        public void Challange(IEvaluationScenario<T> evalluationScenario)
        {
            Fitness = evalluationScenario.Evaluate(Individual);
        }

        public float getNormalizedFitness()
        {
            return Fitness.GetNormalized();
        }

        public AutoEvolutionaryGenomeWarper<T> ChooseMate(List<AutoEvolutionaryGenomeWarper<T>> others)
        {
            return others.MaxBy(o => EvolutionInformtaion.FitnessMatcher.Match(Fitness, o.Fitness));
        }

        public List<Genome> Apariate(AutoEvolutionaryGenomeWarper<T> other)
        {
            var newGenome = WarpedGenome.Apariate(0, other.WarpedGenome, EvolutionInformtaion.ApariateGenDominancePorcentage);
            newGenome = newGenome.Mutate(0, EvolutionInformtaion.MutateGenChooseProbability, EvolutionInformtaion.MutateGenAmplitudePorcentage);
            return new List<Genome> { newGenome };
        }
    }
}
