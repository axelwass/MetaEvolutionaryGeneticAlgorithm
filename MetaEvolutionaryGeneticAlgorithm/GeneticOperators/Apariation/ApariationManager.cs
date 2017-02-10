using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEvolutionaryGeneticAlgorithm.GeneticOperators.Apariation
{
    public class ApariationManager
    {
        static ApariationManager Instance = new ApariationManager();
        List<IApariationResolver> Resolvers = new List<IApariationResolver>();

        public static ApariationManager GetInstance()
        {
            return Instance;
        }

        public void Register(IApariationResolver ar)
        {
            Resolvers.Add(ar);
        }

        public List<Gen> Apariate(int type, List<Gen> mate1, List<Gen> mate2, float dominance_porcentage)
        {
            return Resolvers[type].Apariate(mate1, mate2, dominance_porcentage);
        }

        public int GetApariationTypes()
        {
            return Resolvers.Count;
        }
    }
}
