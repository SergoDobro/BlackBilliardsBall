using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1_2021_02_17_secondASPNET.Repository;

namespace WebApplication1_2021_02_17_secondASPNET
{
    public class PredictionsManager
    {
        private Random random = new Random();
        IPredictionsRepository _predictionsRepository;

        public PredictionsManager(IPredictionsRepository predictionsRepository)
        {
            _predictionsRepository = predictionsRepository;
        }
        public Prediction GetRandomPrediction() 
        { 
            List<Prediction> answers = GetAnswers();
            return answers[random.Next(0,answers.Count-1)];
        }
        public void AddPrediction(string newPrediction) => _predictionsRepository.SavePrediction(newPrediction);
        public void RemovePrediction(string oldPrediction)
        {
            _predictionsRepository.DeletePrediction(new PredictionDto() { PredictionText = oldPrediction });
        }
        public void ChangePrediction(string oldPrediction, string newPrediction)
        {
            _predictionsRepository.UpdatePrediction(new PredictionDto() { PredictionText = oldPrediction }, new PredictionDto() { PredictionText = newPrediction });
        }
        public List<Prediction> GetAnswers() => _predictionsRepository.GetAllPredictions().Select(dto=>new Prediction(dto.PredictionText, dto.Id)).ToList();

    }
}
