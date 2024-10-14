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
            var mappedResult = MapToDto(result);

            if (mappedResult.IsFailed)
            {
                return Result.Fail(mappedResult.Errors);
            }

            var pagedAccounts = mappedResult.Value;
            Person personResult;

            foreach (var account in pagedAccounts.Results)
            {
                try
                {
                    personResult = _personRepository.Get(account.Id);


                    if (personResult != null)
                    {
                        account.Email = personResult.Email;
                    }
                    else
                    {
                        account.Email = "baza@gmail.com";
                    }

                } catch (Exception ex)
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
