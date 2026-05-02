using BankManagement.Core.Entities;
using BankManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;



namespace BankManagement.Infrastructure.Services
{

    namespace BankManagement.Core.Services
    {
        public class AccountService : IAccountService
        {
            private readonly AppDbContext _context;

            // 🔥 Inject DbContext
            public AccountService(AppDbContext context)
            {
                _context = context;
            }

            // 🔹 GET ALL
            public List<Account> GetAll()
            {
                return _context.Accounts.ToList();
            }

            // 🔹 CREATE
            //public string Create(string name, double balance)
            //{
            //    var account = new Account
            //    {
            //        Name = name,
            //        Balance = balance
            //    };

            //    _context.Accounts.Add(account);
            //    _context.SaveChanges();

            //    return "Account Created";

            //}

            //public string Create(string name, double balance)
            //{
            //    _context.Database.ExecuteSqlRaw(
            //        "EXEC CreateAccount @Name, @Balance",
            //        new SqlParameter("@Name", name),
            //        new SqlParameter("@Balance", balance)
            //    );

            //    return "Account Created using Stored Procedure";
            //}
            public string Create(string name, string email, double balance)
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC CreateAccount @Name, @Email, @Balance",
                    new SqlParameter("@Name", name),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Balance", balance)
                );
                //return "Account Created using Stored Procedure";

                 return Ok(new { message = "Account Created" });

                if (string.IsNullOrEmpty(email))
                    return "Email is required";

                if (!email.Contains("@"))
                    return "Invalid email";

                if (balance <= 0)
                    return "Invalid balance";
            }

            private string Ok(object value)
            {
                return System.Text.Json.JsonSerializer.Serialize(value);
            }



            // 🔹 DEPOSIT without stored procedure

            //public string Deposit(int id, double amount)
            //{
            //    var acc = _context.Accounts.FirstOrDefault(x => x.Id == id);

            //    if (acc == null)
            //        return "Account not found";

            //    if (amount <= 0)
            //        return "Invalid amount";

            //    acc.Balance += amount;

            //    _context.SaveChanges();

            //    return $"Deposited {amount}. New Balance: {acc.Balance}";
            //}

            // DEPOSIT with stored procedure
            public string Deposit(int id, double amount)
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC DepositAmount @Id, @Amount",
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Amount", amount)
                );

                _context.Database.ExecuteSqlRaw(
                    "EXEC AddTransaction @AccountId, @Type, @Amount",
                    new SqlParameter("@AccountId", id),
                    new SqlParameter("@Type", "Deposit"),
                    new SqlParameter("@Amount", amount)
                );

                return "Deposit successful";
            }
            // 🔹 WITHDRAW without stored procedure
            //public string Withdraw(int id, double amount)
            //{
            //    var acc = _context.Accounts.FirstOrDefault(x => x.Id == id);

            //    if (acc == null)
            //        return "Account not found";

            //    if (amount <= 0)
            //        return "Invalid amount";

            //    if (acc.Balance < amount)
            //        throw new Exception("Insufficient balance");

            //    acc.Balance -= amount;

            //    _context.SaveChanges();

            //    return $"Withdrawn {amount}. Remaining Balance: {acc.Balance}";
            //}

            // WITHDRAW with stored procedure
            public string Withdraw(int id, double amount)
            {
                _context.Database.ExecuteSqlRaw(
                    "EXEC WithdrawAmount @Id, @Amount",
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Amount", amount)
                );

                _context.Database.ExecuteSqlRaw(
                    "EXEC AddTransaction @AccountId, @Type, @Amount",
                    new SqlParameter("@AccountId", id),
                    new SqlParameter("@Type", "Withdraw"),
                    new SqlParameter("@Amount", amount)
                );

                return "Withdraw successful";
            }
            public string Create(string name, double balance)
            {
                throw new NotImplementedException();
            }
        }

    }
}
