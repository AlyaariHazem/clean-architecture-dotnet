using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.API.Contracts
{
    public enum OpeningBalanceType { Debit = 1, Credit = 2 }

    public record AccountResponseDto(
        Guid AccountId,
        string? AccountName,
        bool IsActive,
        string? Note,
        decimal? OpeningBalance,
        OpeningBalanceType OpeningBalanceType,
        DateTime CreatedAt,
        Guid AccountTypeId,
        string? AccountTypeName
    );

    public partial record CreateAccountRequestDto
    {
        [Required]
        [MinLength(2)]
        public required string AccountName { get; init; }

        public bool IsActive { get; init; } = true;

        public string? Note { get; init; }

        public decimal? OpeningBalance { get; init; }

        public OpeningBalanceType OpeningBalanceType { get; init; } = OpeningBalanceType.Debit;

        [Required]
        public Guid AccountTypeId { get; init; }
    }

    public partial record UpdateAccountRequestDto
    {
        [Required]
        public Guid AccountId { get; init; }

        [Required]
        [MinLength(2)]
        public required string AccountName { get; init; }

        public bool IsActive { get; init; } = true;

        public string? Note { get; init; }

        public decimal? OpeningBalance { get; init; }

        public OpeningBalanceType OpeningBalanceType { get; init; } = OpeningBalanceType.Debit;

        [Required]
        public Guid AccountTypeId { get; init; }
    }

    public record AccountQueryDto(
        string? AccountName,
        bool? IsActive,
        Guid? AccountTypeId
    );

    public record PagedRequestDto(int PageNumber = 1, int PageSize = 10);

    public record GetPagedAccountsRequestDto(
        int PageNumber = 1,
        int PageSize = 10,
        string? AccountName = null,
        bool? IsActive = null,
        Guid? AccountTypeId = null
    );
}
