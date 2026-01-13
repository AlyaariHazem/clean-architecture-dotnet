using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Specifications;

namespace CleanArchitecture.Services
{
    /// <summary>
    /// Account service implementation using Unit of Work pattern.
    /// All database operations are coordinated through Unit of Work for proper transaction management.
    /// </summary>
    public class AccountsService : IAccountsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        private IRepository<Accounts> Repository => _unitOfWork.Repository<Accounts>();

        public async Task<Accounts> CreateAsync(Accounts account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            var createdAccount = await Repository.AddAsync(account);
            await _unitOfWork.SaveChangesAsync();
            return createdAccount;
        }

        public async Task<Accounts?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty", nameof(id));

            var specification = new AccountByIdSpecification(id);
            return await Repository.FirstOrDefaultAsync(specification);
        }

        public async Task<IEnumerable<Accounts>> GetAllAsync(string? accountName = null, bool? state = null, Guid? typeAccountID = null)
        {
            var specification = new AllAccountsSpecification(accountName, state, typeAccountID);
            return await Repository.ToListAsync(specification);
        }

        public async Task<Accounts> UpdateAsync(Accounts account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (account.AccountID == Guid.Empty)
                throw new ArgumentException("Account ID cannot be empty", nameof(account));

            var existingAccount = await Repository.GetByIdAsync<Guid>(account.AccountID);
            if (existingAccount == null)
                throw new KeyNotFoundException($"Account with Id {account.AccountID} not found");

            existingAccount.AccountName = account.AccountName;
            existingAccount.State = account.State;
            existingAccount.Note = account.Note;
            existingAccount.OpenBalance = account.OpenBalance;
            existingAccount.TypeOpenBalance = account.TypeOpenBalance;
            existingAccount.HireDate = account.HireDate;
            existingAccount.TypeAccountID = account.TypeAccountID;

            Repository.Update(existingAccount);
            await _unitOfWork.SaveChangesAsync();
            return existingAccount;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty", nameof(id));

            var account = await Repository.GetByIdAsync<Guid>(id);
            if (account == null)
                return false;

            Repository.Delete(account);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Get paged accounts with optional filtering
        /// </summary>
        public async Task<PagedResult<Accounts>> GetPagedAsync(PaginationParams pagination, string? accountName = null, bool? state = null, Guid? typeAccountID = null)
        {
            var specification = new PagedAccountsSpecification(pagination, accountName, state, typeAccountID);
            return await Repository.ToPagedListAsync(specification, pagination);
        }
    }
}
