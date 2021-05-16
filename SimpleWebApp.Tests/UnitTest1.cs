using System;
using Xunit;
using WebApplication1_2021_02_17_secondASPNET.Repository;

namespace SimpleWebApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            PredictionDto predDto = new PredictionDto() { PredictionText = "yesno 2" };

            IPredictionsRepository predictionsRepository = new PredictionsDatabaseRepository();
            predictionsRepository.SavePrediction(predDto.PredictionText);
        }
        [Fact]
        public void Test2()
        {
            IPredictionsRepository predictionsRepository = new PredictionsDatabaseRepository();
            predictionsRepository.GetPredictionById(2);
            Assert.Throws<Exception>((() => {
                if (predictionsRepository.GetPredictionById(2) != "")
                {
                    throw new Exception();
                }

            }));
        }
        [Fact]
        public void Test3()
        {
            IPredictionsRepository predictionsRepository = new PredictionsDatabaseRepository();
            PredictionDto oldPred = new PredictionDto() { PredictionText = predictionsRepository.GetPredictionById(2) };
            predictionsRepository.UpdatePrediction(oldPred, new PredictionDto() { PredictionText = oldPred.PredictionText + "test" });
            Assert.Throws<Exception>((() => {
                if (predictionsRepository.GetPredictionById(2) != oldPred.PredictionText)
                {
                    throw new Exception();
                }

            }));
        }
        [Fact]
        public void Test4()
        {
            //IPredictionsRepository predictionsRepository = new PredictionsDatabaseRepository();
            //PredictionDto oldPred = new PredictionDto() { PredictionText = predictionsRepository.GetPredictionById(3) };
            //predictionsRepository.DeletePrediction(oldPred);
        }
    }
}
