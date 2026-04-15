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
    }
}