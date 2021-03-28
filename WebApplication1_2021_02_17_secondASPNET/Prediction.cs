using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1_2021_02_17_secondASPNET
{
    public class Prediction
    {
        public string PredictionString { get; set; }
    }
    public class PredictionChanged
    {
        public string OldPredictionString { get; set; }
        public string NewPredictionString { get; set; }
    }
}
