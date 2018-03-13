using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ABVWebApi.Models;
using System.Web.Http.Cors;

namespace ABVWebApi.Controllers
{
    public class BalanceController : ApiController
    {

        private ABVWebApiContext db = new ABVWebApiContext();

        private void Populate<T>(T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }

        // GET: api/Balance/5
        [EnableCors(origins: "http://localhost:9000", headers: "*", methods: "*")]
        [ResponseType(typeof(List<Balance>))]
        public IHttpActionResult GetBalance(int month, int year)
        {
            if (!(month >= 0 && month <= 11 && year>0))
            {
                return NotFound();
            }

            Account[] accounts = db.Accounts.ToArray();

            List<Balance> balances = new List<Balance>();

            foreach (Account account in accounts)
            {
                string accountName = account.AccountName;
                long accountId = account.Id;

                var amount = (from p in db.Transactions
                              where (p.Year == year && p.Month == month && p.AccountId == accountId)
                              select p.Amount).ToArray();

                double adjustedAmount = 0;
                if (amount.Length > 0)
                {
                    adjustedAmount = amount[0];
                }

                Balance balance = new Balance();

                balance.accountName = accountName;
                balance.amount = adjustedAmount;
                balance.month = month;
                //,adjustedAmount);
                //ar amount3 = amount
                balances.Add(balance);
            }

            //var jsonObj = [];

            return Ok(balances);
        }

        // POST: api/Balance
        public void Post([FromBody]string value)
        {
        }

    }
}
