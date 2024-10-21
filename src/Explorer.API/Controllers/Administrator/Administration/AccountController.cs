﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/account")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<PagedResult<AccountDto>> GetAllAccount([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _accountService.GetPagedAccount(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPut("block")]
        public ActionResult<AccountDto> BlockUser([FromBody] AccountDto account)
        {
            var result = _accountService.BlockUser(account);
            return CreateResponse(result);
        }
    }
}
