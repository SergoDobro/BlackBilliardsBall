using Microsoft.AspNetCore.Mvc;

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
    }
}
