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

namespace ABVWebApi.Controllers
{
    public class ReportController : ApiController
    {
        private ABVWebApiContext db = new ABVWebApiContext();

        private string[] accountNames = new string[] { "RandD", "Canteen", "CEOCar", "Marketing", "ParkingFines" };

        // POST: api/Report
        /*public void Post([FromBody]string value)
        {
        }*/

        // GET: api/Transactions/5
        [ResponseType(typeof(Hashtable))]
        public IHttpActionResult GetReport(long year)
        {



            Hashtable report = new Hashtable();

            foreach (string accountName in accountNames)
            {
                var data = from p in db.Transactions
                             where (p.Year == year && p.AccountName == accountName)
                             select p.Amount ;  //new { AccountName = p.Month, Amount = p.Amount };
                report.Add(accountName, data);
            }

            


            /*var categories =from p in db.Transactions
                            group p by p.AccountName into g
                            select new { Category = g.Key, Sum = g.Sum(p => p.Amount) };*/



            // This will raise an exception if entity not found
            // Use SingleOrDefault instead
            //List<Transaction> transactions = query.ToList<Transaction>();



            //report.Canteen = new int[] { 2, 4, 3, 5 };
            /*Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }*/

            return Ok(report);
        }


    }
}
