using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{

    public class AccountService : CrudService<AccountDto, User>, IAccountService
    {
        public readonly ICrudRepository<User> _userRepository;
        public readonly ICrudRepository<Person> _personRepository;

        public AccountService(ICrudRepository<User> userRepository,ICrudRepository<Person> personRepository
            ,  IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
        }

        public new Result<PagedResult<AccountDto>> GetPaged(int page, int pageSize)
        {
            var result = _userRepository.GetPaged(page, pageSize);
            var mappedResults = MapToDto(result);

            if (mappedResults.IsFailed)
            {
                return Result.Fail(mappedResults.Errors);
            }

            var pagedAccounts = mappedResults.Value;
            Person? personResult;
            User? userResult;

            foreach (var account in pagedAccounts.Results)
            {
                try
                {
                    userResult = result.Results.Find(x => x.Username == account.Username);
                    personResult = _personRepository.GetPaged(0,0).Results.Find(p => p.UserId == userResult.Id);

                    account.Email = personResult?.Email ?? "N/A";

                }
                catch (Exception ex)
                {
                    if (account.Role != UserRole.Administrator.ToString())
                    {
                        return Result.Fail($"An error occurred while retrieving the person for account " +
                            $"{account.Id}: {ex.Message}");
                    }

                    account.Email = "N/A";
                    continue;
                }
            }

            return Result.Ok(pagedAccounts);
        }


        public Result<AccountDto> BlockUser(AccountDto account)
        {
            if (!account.IsActive)
            {
                return account;
            }

            account.IsActive = false;
            var updateResult = Update(account);

            if (updateResult.IsSuccess)
            {
                return Result.Ok(account);
            }

            return Result.Fail(updateResult.Errors);
        }
    }
}
