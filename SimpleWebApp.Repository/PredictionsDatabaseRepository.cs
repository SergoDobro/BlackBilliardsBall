using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WebApplication1_2021_02_17_secondASPNET.Repository
{
    public class PredictionsDatabaseRepository : IPredictionsRepository
    {
        public string GetPredictionById(int predictionId)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            //string sqlQuery = "SELECT * from predictions";
            //PredictionDto[] arr = db.Query<PredictionDto>(sqlQuery).OrderBy(new Func<PredictionDto, int>((x) => { return x.Id; })).ToArray();
            //if (arr.Length>predictionId)
            //{
            //    return arr[predictionId - 1].PredictionText;
            //}
            //else
            //{
            //    return ""; 
            //}
            var prediction = db.Query<PredictionDto>("Select * From predictions WHERE id =" + predictionId).FirstOrDefault();// ?? new PredictionDto() { Id = 0, PredictionText = ""};
            return prediction.PredictionText;
        }

        public void SavePrediction(string prediction)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            string sqlQuery = "INSERT INTO predictions (predictionText) Values(@PredictionText)";

            int rowsAffected = db.Execute(sqlQuery, prediction);
        }
        public void UpdatePrediction(PredictionDto oldPrediction, PredictionDto newPrediction)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            //db.Execute("update predictions set predictionText = '" + newPrediction.PredictionText + "' where predictionText = @predictionText", oldPrediction);
            db.Execute("update predictions set predictionText = @newPred where predictionText = @oldPred", new { oldPred = oldPrediction.PredictionText, newPred = newPrediction.PredictionText });
        }
        public void DeletePrediction(PredictionDto prediction)
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            db.Execute("DELETE from predictions where predictionText = @PredictionText", new { PredictionText = prediction.PredictionText });
        }

        public List<PredictionDto> GetAllPredictions()
        {
            using IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=simplewebapp;Uid=root;Pwd=my-secret-pw;");
            return db.Query<PredictionDto>("Select * From predictions").ToList();
        }
    }
}
