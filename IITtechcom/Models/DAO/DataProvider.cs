using IITtechcom.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITtechcom.Models.DAO
{
    public class DataProvider
    {
        private static ModelDbContext db;

        private DataProvider() { }

        public static ModelDbContext GetInstance()
        {
            if(db == null)
            {
                db = new ModelDbContext();
            }

            return db;
        }
    }
}