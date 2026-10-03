using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Models.Statistics
{
    public class StatisticSummary
    {
        public int TotalAccounts { get; set; }

        public decimal TotalBalance { get; set; }

        public int TotalTransactions { get; set; }

        public int TotalCustomers { get; set; }
    }
}