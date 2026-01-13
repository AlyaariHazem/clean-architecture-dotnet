using CleanArchitecture.API.Contracts;
using CleanArchitecture.Core.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IAccountsService accountsService, ILogger<AccountsController> logger)
        {
            _accountsService = accountsService ?? throw new ArgumentNullException(nameof(accountsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all accounts with optional filtering (POST endpoint)
        /// </summary>
        /// <param name="request">Filter criteria</param>
        /// <returns>List of all accounts matching the filters</returns>
        [HttpPost("get-all")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<AccountResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<IEnumerable<AccountResponseDto>>>> GetAllAccounts([FromBody] AccountQueryDto? request = null)
        {
            var accounts = await _accountsService.GetAllAsync(
                accountName: request?.AccountName,
                state: request?.IsActive,
                typeAccountID: request?.AccountTypeId
            );
            
            var response = accounts.Select(account => MapToDto(account));
            return Ok(ApiResponse<IEnumerable<AccountResponseDto>>.SuccessResponse(response, "Accounts retrieved successfully", StatusCodes.Status200OK));
        }

        /// <summary>
        /// Get paged accounts with optional filtering (POST endpoint)
        /// </summary>
        /// <param name="request">Pagination and filter criteria</param>
        /// <returns>Paged list of accounts</returns>
        [HttpPost("get-paged")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<AccountResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PagedResult<AccountResponseDto>>>> GetPagedAccounts([FromBody] GetPagedAccountsRequestDto request)
        {
            if (request == null)
            {
                throw new BadRequestException("Request body is required");
            }

            if (request.PageNumber < 1)
            {
                throw new BadRequestException("PageNumber must be greater than zero");
            }

            if (request.PageSize < 1)
            {
                throw new BadRequestException("PageSize must be greater than zero");
            }

            var paginationParams = new PaginationParams
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var pagedResult = await _accountsService.GetPagedAsync(
                paginationParams,
                accountName: request.AccountName,
                state: request.IsActive,
                typeAccountID: request.AccountTypeId
            );

            var response = new PagedResult<AccountResponseDto>
            {
                Items = pagedResult.Items.Select(account => MapToDto(account)).ToList(),
                TotalCount = pagedResult.TotalCount,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize
            };

            return Ok(ApiResponse<PagedResult<AccountResponseDto>>.SuccessResponse(response, "Accounts retrieved successfully", StatusCodes.Status200OK));
        }

        /// <summary>
        /// Get an account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>Account details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<AccountResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<AccountResponseDto>>> GetAccountById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BadRequestException("Account ID cannot be empty");
            }

            var account = await _accountsService.GetByIdAsync(id);
            if (account == null)
            {
                throw new NotFoundException($"Account with ID {id} not found");
            }

            return Ok(ApiResponse<AccountResponseDto>.SuccessResponse(MapToDto(account), "Account retrieved successfully", StatusCodes.Status200OK));
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="request">Account creation request</param>
        /// <returns>Created account</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AccountResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<AccountResponseDto>>> CreateAccount([FromBody] CreateAccountRequestDto request)
        {
            var account = new Accounts
            {
                AccountName = request.AccountName,
                State = request.IsActive,
                Note = request.Note,
                OpenBalance = request.OpeningBalance,
                TypeOpenBalance = request.OpeningBalanceType == OpeningBalanceType.Debit,
                HireDate = DateTime.Now,
                TypeAccountID = request.AccountTypeId
            };

            var createdAccount = await _accountsService.CreateAsync(account);
            var response = MapToDto(createdAccount);

            return CreatedAtAction(
                nameof(GetAccountById),
                new { id = createdAccount.AccountID },
                ApiResponse<AccountResponseDto>.SuccessResponse(response, "Account created successfully", StatusCodes.Status201Created)
            );
        }

        /// <summary>
        /// Update an existing account
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="request">Account update request</param>
        /// <returns>Updated account</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<AccountResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<AccountResponseDto>>> UpdateAccount(Guid id, [FromBody] UpdateAccountRequestDto request)
        {
            if (id != request.AccountId)
            {
                throw new BadRequestException("ID in URL does not match ID in request body");
            }

            if (id == Guid.Empty)
            {
                throw new BadRequestException("Account ID cannot be empty");
            }

            var account = new Accounts
            {
                AccountID = request.AccountId,
                AccountName = request.AccountName,
                State = request.IsActive,
                Note = request.Note,
                OpenBalance = request.OpeningBalance,
                TypeOpenBalance = request.OpeningBalanceType == OpeningBalanceType.Debit,
                HireDate = DateTime.Now,
                TypeAccountID = request.AccountTypeId
            };

            var updatedAccount = await _accountsService.UpdateAsync(account);
            var response = MapToDto(updatedAccount);

            return Ok(ApiResponse<AccountResponseDto>.SuccessResponse(response, "Account updated successfully", StatusCodes.Status200OK));
        }

        /// <summary>
        /// Delete an account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BadRequestException("Account ID cannot be empty");
            }

            var deleted = await _accountsService.DeleteAsync(id);
            if (!deleted)
            {
                throw new NotFoundException($"Account with ID {id} not found");
            }

            return NoContent();
        }

        private static AccountResponseDto MapToDto(Accounts account)
        {
            return new AccountResponseDto(
                AccountId: account.AccountID,
                AccountName: account.AccountName,
                IsActive: account.State,
                Note: account.Note,
                OpeningBalance: account.OpenBalance,
                OpeningBalanceType: account.TypeOpenBalance ? OpeningBalanceType.Debit : OpeningBalanceType.Credit,
                CreatedAt: account.HireDate,
                AccountTypeId: account.TypeAccountID,
                AccountTypeName: account.TypeAccount?.TypeAccountName
            );
        }
    }
}
