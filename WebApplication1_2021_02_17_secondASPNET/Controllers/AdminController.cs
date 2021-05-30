using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication1_2021_02_17_secondASPNET.Controllers
{
    [ApiController]
    //[Route("{controller}/{action}")]
    public class AdminController : ControllerBase
    {
        PredictionsManager _predictionsManager;
        public AdminController(PredictionsManager predictionsManager)
        {
            _predictionsManager = predictionsManager;
        }
        [HttpGet]
        [Route("getAnswers")]
        public ActionResult GetPredictions()
        {
            Prediction responseData = _predictionsManager.GetRandomPrediction();
            return Ok(responseData);
        }
        [HttpPost]
        [Route("addPrediction")]
        public ActionResult AddPrediction([FromBody] Prediction prediction)
        {
            //TryValidateModel(prediction);
            if (prediction == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                _predictionsManager.AddPrediction(prediction.PredictionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
        [HttpPost]
        [Route("removePrediction")]
        public ActionResult RemovePrediction([FromBody] Prediction prediction)
        {
            _predictionsManager.RemovePrediction(prediction.PredictionString);
            return Ok();
        }
        [HttpPut]
        [Route("changePrediction")]
        public ActionResult ChangePrediction([FromBody] PredictionChanged prediction)
        {
            _predictionsManager.ChangePrediction(prediction.OldPredictionString, prediction.NewPredictionString);
            return Ok();
        }

    }
}
