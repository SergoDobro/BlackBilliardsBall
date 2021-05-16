using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1_2021_02_17_secondASPNET.Repository;
namespace WebApplication1_2021_02_17_secondASPNET.Repository
{
    public class PredictionDatabaseInMemory : IPredictionsRepository
    {
        List<PredictionDto> predictions = new List<PredictionDto>()
        {
            new PredictionDto(){ PredictionText="Тебе сегодня повезет!" }};
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

        public List<PredictionDto> GetAllPredictions() => predictions;

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
        public void SavePrediction(string prediction)
        {
            predictions.Add(new PredictionDto() { PredictionText = prediction });
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
