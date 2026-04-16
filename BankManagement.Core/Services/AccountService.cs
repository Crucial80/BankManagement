using System;
using System.Collections.Generic;
using System.Text;

using BankManagement.Core.Entities;
using BankManagement.Core.Interfaces;

namespace BankManagement.Core.Services
{
    public class AccountService : IAccountService
    {
        private List<Account> accounts = new List<Account>();

        public List<Account> GetAll()
        {
            return accounts;
        }

        public string Create(string name, double balance)
        {
            accounts.Add(new Account
            {
                Id = accounts.Count + 1,
                Name = name,
                Balance = balance
            });

            return "Account Created";
        }



        public string Deposit(int id, double amount)
        {
            var acc = accounts.FirstOrDefault(x => x.Id == id);

            if (acc == null)
                return "Account not found";

            if (amount <= 0)
                return "Invalid amount";

            acc.Balance += amount;

            return $"Deposited {amount}. New Balance: {acc.Balance}";
        }




        public string Withdraw(int id, double amount)
        {
            var acc = accounts.FirstOrDefault(x => x.Id == id);

            if (acc == null)
                return "Account not found";

            if (amount <= 0)
                return "Invalid amount";

            if (acc.Balance < amount)
                throw new Exception("Insufficient balance");

            acc.Balance -= amount;

            return $"Withdrawn {amount}. Remaining Balance: {acc.Balance}";

        }
    }
}
