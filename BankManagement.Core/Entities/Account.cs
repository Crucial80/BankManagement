using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagement.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
    }
}