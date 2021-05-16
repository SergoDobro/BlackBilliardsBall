using System.Collections.Generic;

namespace WebApplication1_2021_02_17_secondASPNET.Repository
{
    public interface IPredictionsRepository
    {
        void SavePrediction(string prediction);
        string GetPredictionById(int predictionId);
        void UpdatePrediction(PredictionDto oldPrediction, PredictionDto newPrediction);
        void DeletePrediction(PredictionDto prediction);

        List<PredictionDto> GetAllPredictions();
    }
}
