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

        public AccountController(IAccountService service)
        {
            _service = service;
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


        [HttpPost("deposit")]
        public string Deposit(int id, double amount)
        {
            return _service.Deposit(id, amount);
        }

        [HttpPost("withdraw")]
        public string Withdraw(int id, double amount)
        {
            try
            {
                return _service.Withdraw(id, amount);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }



}