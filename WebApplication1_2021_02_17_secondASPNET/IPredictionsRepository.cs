﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1_2021_02_17_secondASPNET
{
    public interface IPredictionsRepository
    {
        void SavePrediction(Prediction prediction);
    }
}
