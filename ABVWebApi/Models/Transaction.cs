using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABVWebApi.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public DateTime EnteredDateTime { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string AccountName { get; set; }
        public float Amount { get; set; }
    }
}