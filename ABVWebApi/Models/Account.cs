using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ABVWebApi.Models
{
    public class Account
    {
       //[Key]
        public int Id { get; set; }

        public string AccountName { get; set; }

        public string AccountDisplayName { get; set; }
    }
}