using System;
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
    public class TransactionsController : ApiController
    {
        private ABVWebApiContext db = new ABVWebApiContext();

        // GET: api/Transactions
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof(Balance[]))]
        public IHttpActionResult GetTransactions(int month,int year)
        {


            List<Account> accounts = db.Accounts.ToList();

            List<Balance> balances = new List<Balance>();

            if (!(month >= 0 && month <= 11 && year > 0))
            {
                return NotFound();
            }

            List<Transaction> transactions = (from p in db.Transactions
                                              where (p.Year == year && p.Month == month)
                                              select p).ToList();

            foreach (Transaction transaction in transactions)
            {
                Account[] accountList = accounts.Where(p => (p.Id == transaction.AccountId)).ToArray();

                Account account = accountList[0];
                Balance balance = new Balance();
                balance.accountName = account.AccountDisplayName;
                balance.accountId = account.Id;
                balance.transactionId = transaction.Id;
                balance.amount = transaction.Amount;
                balance.enteredDateTime = transaction.EnteredDateTime;
                balance.month = month;
                balance.year = year;


                balances.Add(balance);

            }




            return Ok(balances);
        }

        // GET: api/Transactions/5
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult GetTransaction(long id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // PUT: api/Transactions/5
        [EnableCors(origins: "http://localhost:9000", headers: "*", methods: "*")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransaction(long id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.Id)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Transactions
        [EnableCors(origins: "http://localhost:9000", headers: "*", methods: "*")]
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            transaction.EnteredDateTime = DateTime.Now;

            try
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = transaction.Id }, transaction);
            } catch(Exception e) {
                return BadRequest(ModelState);
            }
            
        }

        // DELETE: api/Transactions/5
        [EnableCors(origins: "http://localhost:9000", headers: "*", methods: "*")]
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult DeleteTransaction(long id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }


            try
            {
                db.Transactions.Remove(transaction);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = transaction.Id }, transaction);
            }
            catch (Exception e)
            {
                return BadRequest(ModelState);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(long id)
        {
            return db.Transactions.Count(e => e.Id == id) > 0;
        }
    }
}