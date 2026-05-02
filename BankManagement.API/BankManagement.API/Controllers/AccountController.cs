using BankManagement.API.DTOs;
using BankManagement.Core.Entities;
using BankManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace BankManagement.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly AppDbContext _context;

        public AccountController(IAccountService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _service.GetAll();
            return Ok(data);
        }

        [HttpGet("transactions/{accountId}")]
        public IActionResult GetTransactions(int accountId)
        {
            var data = _context.Transactions
                .FromSqlRaw("EXEC GetTransactions @AccountId",
                    new SqlParameter("@AccountId", accountId))
                .ToList();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(AccountDto dto)
        {
            var result = _service.Create(dto.Name, dto.Email, dto.Balance);
            return Ok(result);
        }

        [HttpPost("deposit")]
        public IActionResult Deposit(int id, double amount)
        {
            var result = _service.Deposit(id, amount);
            return Ok(result);
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw(int id, double amount)
        {
            try
            {
                var result = _service.Withdraw(id, amount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    internal class Transaction
    {
    }
}