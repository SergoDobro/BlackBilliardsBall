﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1_2021_02_17_secondASPNET
{
    public class PredictionsManager
    {
        private Random random = new Random();
        static private List<string> answers = new List<string>() { "Use the force", "Maybe", "Choose by yourself", "42",
        "Another question: What does the fox say?", "I think no...", "Yes!", "No", "No idea"};
        public string GetRandomPrediction() => answers[random.Next(0, answers.Count)];
        public void AddPrediction(string newPrediction) => answers.Add(newPrediction);

    }
}
