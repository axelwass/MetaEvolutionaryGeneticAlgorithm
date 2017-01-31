using AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.Problem;
using System.Collections.Generic;

namespace AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.GA
{
    interface IGenomeWarper<T, U> where U : IGenomeWarper<T,U>
    {
        List<Genome> Apariate(U other);
        void Challange(IEvaluationScenario<T> evalluationScenario);
    }
}
