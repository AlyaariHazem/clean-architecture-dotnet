using CleanArchitecture.API.Contracts;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];
        private readonly ICarServices _carServices;

        public WeatherForecastController(ICarServices carServices)
        {
            _carServices = carServices;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost("/testing")]
        public async Task<ActionResult<CarsContract>> CreateNew([FromBody] CarsOperationsContract model)
        {
            var oDataModel = new Cars()
            {
                CarName = model.CarName,
            };
            var oResult = await _carServices.CreateAsync(oDataModel);

            var oDataResult = new CarsContract()
            {
                CarName = oDataModel.CarName,
            };

            return Ok(oDataResult);

        }
    }
}


