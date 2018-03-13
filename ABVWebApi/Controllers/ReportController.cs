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
    public class ReportController : ApiController
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

        // GET: api/Report/5
        [EnableCors(origins: "http://localhost:9000", headers: "*", methods: "*")]
        [ResponseType(typeof(Hashtable))]
        public IHttpActionResult GetReport(long year)
        {
            Hashtable report = new Hashtable();

            foreach (string accountName in accountNames)
            {

                var data = from p in db.Transactions
                            where (p.Year == year && p.AccountName == accountName)
                            group p by p.Month into g
                            select new
                            {
                                Month = g.Key,
                                Amount = g.Sum(p => p.Amount)
                            };

                float[] arr = new float[12];
                Populate(arr, 0);
                foreach (var item in data)
                {
                    arr[item.Month] = item.Amount;
                }
                report.Add(accountName, arr);
            }

            return Ok(report);
        }


    }
}
