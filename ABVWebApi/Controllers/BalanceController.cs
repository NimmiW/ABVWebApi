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

        private string[] accountNames = new string[] { "RandD", "Canteen", "CEOCar", "Marketing", "ParkingFines" };


        private void Populate<T>(T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }

        // GET: api/Balance/5
        [EnableCors(origins: "http://localhost:9000", headers: "*", methods: "*")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetBalance(int month, int year)
        {
            if (!(month >= 0 && month <= 11 && year>0))
            {
                return NotFound();
            }

            Hashtable accountBalances = new Hashtable();

            foreach (string accountName in accountNames)
            {

                var amount = from p in db.Transactions
                             where (p.Year == year && p.Month == month && p.AccountName == accountName)
                             group p by p.AccountName into g
                             select new
                             {
                                 Amount = g.Sum(p => p.Amount)
                             };

                /*float[] arr = new float[12];
                Populate(arr, 0);
                foreach (var item in data)
                {
                    arr[Int32.Parse(item.Month)] = item.Amount;

                }*/
                accountBalances.Add(accountName, amount);
            }

            return Ok(accountBalances);
        }

        // POST: api/Balance
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Balance/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
