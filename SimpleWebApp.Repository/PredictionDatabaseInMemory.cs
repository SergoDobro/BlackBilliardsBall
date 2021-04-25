using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1_2021_02_17_secondASPNET.Repository;
namespace WebApplication1_2021_02_17_secondASPNET.Repository
{
    class PredictionDatabaseInMemory : IPredictionsRepository
    {
        List<PredictionDto> predictions = new List<PredictionDto>();
        public void DeletePrediction(PredictionDto prediction)
        {
            for (int i = 0; i < predictions.Count; i++)
            {
                if (predictions[i].PredictionText == prediction.PredictionText)
                {
                    predictions.RemoveAt(i);
                    i--;
                }
            }
        }
        public string GetPredictionById(int predictionId)
        {
            for (int i = 0; i < predictions.Count; i++)
            {
                if (predictions[i].Id == predictionId)
                {
                    return predictions[i].PredictionText;
                }
            }
            return predictions[0].PredictionText;
        }
        public void SavePrediction(PredictionDto prediction)
        {
            predictions.Add(prediction);
        }
        public void UpdatePrediction(PredictionDto oldPrediction, PredictionDto newPrediction)
        {
            for (int i = 0; i < predictions.Count; i++)
            {
                if (predictions[i].PredictionText == oldPrediction.PredictionText)
                {
                    predictions[i].PredictionText = newPrediction.PredictionText;
                }
            }
        }
    }
}
