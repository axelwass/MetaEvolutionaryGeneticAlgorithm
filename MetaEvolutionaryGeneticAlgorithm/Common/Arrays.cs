using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEvolutionaryGeneticAlgorithm.Common
{
    public static class Arrays
    {
        public static string PrettyPrintMatrix<T>(T[,] matrix)
        {
            if (matrix == null)
                return "";

            var str = "[";

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                str += "[";
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    str += matrix[i, j] + ",";
                }
                str += "]";
            }
            str += "]";

            return str;
        }
    }
}
