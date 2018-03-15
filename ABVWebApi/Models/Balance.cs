using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABVWebApi.Models
{
    public class Balance
    {

        public long transactionId;
        public long accountId;
        public int month;
        public int year;
        public string accountName;
        public double amount;
        public DateTime enteredDateTime;
    }
}