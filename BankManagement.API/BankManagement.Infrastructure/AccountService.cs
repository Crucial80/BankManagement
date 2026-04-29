using BankManagement.Core.Entities;
using BankManagement.Core.Interfaces;



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
            public string Create(string name, double balance)
            {
                var account = new Account
                {
                    Name = name,
                    Balance = balance
                };

                _context.Accounts.Add(account);
                _context.SaveChanges();

                return "Account Created";
            }

            // 🔹 DEPOSIT
            public string Deposit(int id, double amount)
            {
                var acc = _context.Accounts.FirstOrDefault(x => x.Id == id);

                if (acc == null)
                    return "Account not found";

                if (amount <= 0)
                    return "Invalid amount";

                acc.Balance += amount;

                _context.SaveChanges();

                return $"Deposited {amount}. New Balance: {acc.Balance}";
            }

            // 🔹 WITHDRAW
            public string Withdraw(int id, double amount)
            {
                var acc = _context.Accounts.FirstOrDefault(x => x.Id == id);

                if (acc == null)
                    return "Account not found";

                if (amount <= 0)
                    return "Invalid amount";

                if (acc.Balance < amount)
                    throw new Exception("Insufficient balance");

                acc.Balance -= amount;

                _context.SaveChanges();

                return $"Withdrawn {amount}. Remaining Balance: {acc.Balance}";
            }

        }
    }
}
