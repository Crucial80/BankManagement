using BankManagement.API.DTOs;
using BankManagement.Core.Interfaces;
using BankManagement.Core.Services;
using BankManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController()
        {
            _service = new AccountService();
        }

        [HttpGet]
        public List<Account> Get()
        {
            return _service.GetAll();
        }

        [HttpPost]
        public string Create(AccountDto dto)
        {
            return _service.Create(dto.Name, dto.Balance);
        }
    }
}