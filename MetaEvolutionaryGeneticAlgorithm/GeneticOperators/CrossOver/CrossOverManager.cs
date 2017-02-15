using MetaEvolutionaryGeneticAlgorithm.BaseGeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEvolutionaryGeneticAlgorithm.GeneticOperators.CrossOver
{
    public class CrossOverManager
    {
        static CrossOverManager Instance = new CrossOverManager();
        List<ICrossOverResolver> Resolvers = new List<ICrossOverResolver>();

        public static CrossOverManager GetInstance()
        {
            return Instance;
        }

        public void Register(ICrossOverResolver ar)
        {
            Resolvers.Add(ar);
        }

        public List<Gen> Cross(int type, List<Gen> mate1, List<Gen> mate2, float dominance_porcentage)
        {
            return Resolvers[type].Cross(mate1, mate2, dominance_porcentage);
        }

        public int GetCrossOverTypes()
        {
            return Resolvers.Count;
        }
    }
}
