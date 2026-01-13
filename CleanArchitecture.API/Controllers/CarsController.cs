using CleanArchitecture.API.Contracts;
using CleanArchitecture.Core.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
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
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CarResponseDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<CarResponseDto>>>> GetAllCars()
        {
            var cars = await _carServices.GetAllAsync();
            var response = cars.Select(car => MapToDto(car));
            return Ok(ApiResponse<IEnumerable<CarResponseDto>>.SuccessResponse(response, "Cars retrieved successfully"));
        }

        /// <summary>
        /// Get a car by ID
        /// </summary>
        /// <param name="id">Car ID</param>
        /// <returns>Car details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CarResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<CarResponseDto>>> GetCarById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Car ID must be greater than zero");
            }

            var car = await _carServices.GetByIdAsync(id);
            if (car == null)
            {
                throw new NotFoundException("Car", id);
            }

            return Ok(ApiResponse<CarResponseDto>.SuccessResponse(MapToDto(car), "Car retrieved successfully"));
        }

        /// <summary>
        /// Create a new car
        /// </summary>
        /// <param name="request">Car creation request</param>
        /// <returns>Created car</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CarResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<ApiResponse<CarResponseDto>>> CreateCar([FromBody] CreateCarRequestDto request)
        {
            // ModelState validation is handled by ValidationFilterAttribute
            // Additional business validation can be added here

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

            return CreatedAtAction(
                nameof(GetCarById),
                new { id = createdCar.Id },
                ApiResponse<CarResponseDto>.SuccessResponse(response, "Car created successfully", 201)
            );
        }

        /// <summary>
        /// Update an existing car
        /// </summary>
        /// <param name="id">Car ID</param>
        /// <param name="request">Car update request</param>
        /// <returns>Updated car</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CarResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<ApiResponse<CarResponseDto>>> UpdateCar(int id, [FromBody] UpdateCarRequestDto request)
        {
            if (id != request.Id)
            {
                throw new BadRequestException("ID in URL does not match ID in request body");
            }

            // ModelState validation is handled by ValidationFilterAttribute

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

            return Ok(ApiResponse<CarResponseDto>.SuccessResponse(response, "Car updated successfully"));
        }

        /// <summary>
        /// Delete a car by ID
        /// </summary>
        /// <param name="id">Car ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> DeleteCar(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Car ID must be greater than zero");
            }

            var deleted = await _carServices.DeleteAsync(id);
            if (!deleted)
            {
                throw new NotFoundException("Car", id);
            }

            return Ok(ApiResponse.SuccessResponse("Car deleted successfully", 200));
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
