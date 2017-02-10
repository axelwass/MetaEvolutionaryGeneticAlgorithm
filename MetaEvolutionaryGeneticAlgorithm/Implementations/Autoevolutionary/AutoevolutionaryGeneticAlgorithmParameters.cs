using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.GA;
using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm.Interface.Problem;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary;
using MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary.FitnessMAtcher.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEvolutionaryGeneticAlgorithm.Implementations.Autoevolutionary
{
    public class AutoevolutionaryGeneticAlgorithmParameters<T,U>:GeneticAlgorithmInformation<T> where U : IAutoEvolutionaryFitnessMatcher
    {
        public AutoEvolutionaryInformationFabrik<U> EvolutionaryInformationFabrik;
        public int InitialLives;
        public float AprovedPorcentage;
        public int MaxPopulation;
        public int DeathTournamentCount;
        public int ApariateTournamentCount;
        public int ForeingersByGeneration;
    }
}
