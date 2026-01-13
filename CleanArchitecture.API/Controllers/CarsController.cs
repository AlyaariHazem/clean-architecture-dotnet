using CleanArchitecture.API.Contracts;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CarsController : ControllerBase
    {
        private readonly ICarServices _carServices;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarServices carServices, ILogger<CarsController> logger)
        {
            _carServices = carServices ?? throw new ArgumentNullException(nameof(carServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all cars
        /// </summary>
        /// <returns>List of all cars</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CarResponseDto>>> GetAllCars()
        {
            try
            {
                var cars = await _carServices.GetAllAsync();
                var response = cars.Select(car => MapToDto(car));
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all cars");
                return StatusCode(500, "An error occurred while retrieving cars");
            }
        }

        /// <summary>
        /// Get a car by ID
        /// </summary>
        /// <param name="id">Car ID</param>
        /// <returns>Car details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CarResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarResponseDto>> GetCarById(int id)
        {
            try
            {
                var car = await _carServices.GetByIdAsync(id);
                if (car == null)
                {
                    return NotFound($"Car with ID {id} not found");
                }

                return Ok(MapToDto(car));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid ID provided: {Id}", id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving car with ID {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the car");
            }
        }

        /// <summary>
        /// Create a new car
        /// </summary>
        /// <param name="request">Car creation request</param>
        /// <returns>Created car</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CarResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarResponseDto>> CreateCar([FromBody] CreateCarRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var car = new Cars
                {
                    CarName = request.CarName,
                    CarType = request.CarType,
                    Manufacturer = request.Manufacturer,
                    Year = request.Year,
                    Price = request.Price
                };

                var createdCar = await _carServices.CreateAsync(car);
                var response = MapToDto(createdCar);

                return CreatedAtAction(nameof(GetCarById), new { id = createdCar.Id }, response);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Null car provided for creation");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating car");
                return StatusCode(500, "An error occurred while creating the car");
            }
        }

        /// <summary>
        /// Update an existing car
        /// </summary>
        /// <param name="id">Car ID</param>
        /// <param name="request">Car update request</param>
        /// <returns>Updated car</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CarResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarResponseDto>> UpdateCar(int id, [FromBody] UpdateCarRequestDto request)
        {
            try
            {
                if (id != request.Id)
                {
                    return BadRequest("ID in URL does not match ID in request body");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var car = new Cars
                {
                    Id = request.Id,
                    CarName = request.CarName,
                    CarType = request.CarType,
                    Manufacturer = request.Manufacturer,
                    Year = request.Year,
                    Price = request.Price
                };

                var updatedCar = await _carServices.UpdateAsync(car);
                var response = MapToDto(updatedCar);

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Car with ID {Id} not found for update", id);
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid argument provided for update: {Id}", id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating car with ID {Id}", id);
                return StatusCode(500, "An error occurred while updating the car");
            }
        }

        /// <summary>
        /// Delete a car by ID
        /// </summary>
        /// <param name="id">Car ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                var deleted = await _carServices.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound($"Car with ID {id} not found");
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid ID provided for deletion: {Id}", id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting car with ID {Id}", id);
                return StatusCode(500, "An error occurred while deleting the car");
            }
        }

        private static CarResponseDto MapToDto(Cars car)
        {
            return new CarResponseDto
            {
                Id = car.Id,
                CarName = car.CarName,
                CarType = car.CarType,
                Manufacturer = car.Manufacturer,
                Year = car.Year,
                Price = car.Price,
                CreatedAt = car.CreatedAt,
                UpdatedAt = car.UpdatedAt
            };
        }
    }
}
