﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmoGeneticoAutoevolutivo.BaseGeneticAlgorithm.Interface.Problem
{
    interface IFitness
    {
        float GetNormalized();
    }
}
