using System;
using System.Collections.Generic;
using System.Text;

using BankManagement.Core.Entities;

namespace BankManagement.Core.Interfaces
{
    public interface IAccountService
    {
        List<Account> GetAll();
        string Create(string name, double balance);
    }
}