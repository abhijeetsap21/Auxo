using NewLetter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using System.Web;


namespace NewLetter.Areas.Admin.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;
        private readonly oriondbEntities _db;


        public UnitOfWork()
        {
            _db = new oriondbEntities();           
        }

        public void Dispose()
        {

        }

        public void StartTransaction()
        {
            _transaction = new TransactionScope();
        }

        public void Commit()
        {
            _db.SaveChanges();
            _transaction.Complete();
          
        }

        public DbContext Db
        {
            get { return _db; }
        }
       

    }
}
