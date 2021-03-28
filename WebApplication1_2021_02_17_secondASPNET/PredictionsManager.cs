using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1_2021_02_17_secondASPNET
{
    public class PredictionsManager
    {
        private Random random = new Random();
        private List<string> answers = new List<string>() { "Use the force", "Maybe", "Choose by yourself", "42",
        "Another question: What does the fox say?", "I think no...", "Yes!", "No", "No idea"};
        public string GetRandomPrediction() => answers[random.Next(0, answers.Count)];
        public void AddPrediction(string newPrediction) => answers.Add(newPrediction);
        public void RemovePrediction(string oldPrediction)
        {
            if (answers.Contains(oldPrediction))
            {
                answers.Remove(oldPrediction);
            }
        }
        public void ChangePrediction(string oldPrediction, string newPrediction)
        {
            if (answers.Contains(oldPrediction))
            {
                for (int i = 0; i < answers.Count; i++)
                {
                    if (answers[i] == oldPrediction)
                    {
                        answers[i] = newPrediction;
                    }
                }
            }
        }
        public List<string> GetAnswers() => answers;

    }
}
