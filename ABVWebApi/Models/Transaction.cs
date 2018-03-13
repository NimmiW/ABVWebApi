using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABVWebApi.Models
{
    public class Transaction
    {

        public int Id { get; set; }
        public DateTime EnteredDateTime { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        //[ForeignKey("Account")]
        public int AccountId { get; set; }
        public double Amount { get; set; }

        //public virtual Account Account { get; set; }



    }
}