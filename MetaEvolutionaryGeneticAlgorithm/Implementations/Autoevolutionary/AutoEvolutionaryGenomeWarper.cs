using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.GA;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary
{
    public class AutoEvolutionaryGenomeWarper<T, U> : IGenomeWarper<T, AutoEvolutionaryGenomeWarper<T,U>> where U : IAutoEvolutionaryFitnessMatcher
    {
        int Lives;
        public IFitness Fitness;
        public Genome WarpedGenome;
        public T Individual;
        public AutoevolutionaryInformation EvolutionInformtaion;

        public AutoEvolutionaryGenomeWarper(IIndividualFabrik<T> individualFabrik, AutoEvolutionaryInformationFabrik<U> autoEvolutionaryInformationFabrik, Genome genome, int initialLives)
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
            return Fitness != null? Fitness.GetNormalized() : 0;
        }

        public AutoEvolutionaryGenomeWarper<T,U> ChooseMate(List<AutoEvolutionaryGenomeWarper<T,U>> others)
        {
            return others.MaxBy(o => EvolutionInformtaion.FitnessMatcher.Match(Fitness, o.Fitness));
        }

        public List<Genome> Apariate(AutoEvolutionaryGenomeWarper<T,U> other)
        {
            List<Genome> children = new List<Genome>();
            for(int i=0; i<3; i++)
            {
                var child = WarpedGenome.Apariate(EvolutionInformtaion.GetApariationType(), other.WarpedGenome, EvolutionInformtaion.ApariateGenDominancePorcentage);
                child = child.Mutate(EvolutionInformtaion.GetMutationType(), EvolutionInformtaion.MutateGenChooseProbability, EvolutionInformtaion.MutateGenAmplitudePorcentage);
                children.Add(child);
            }
            return children;
        }
    }
}
