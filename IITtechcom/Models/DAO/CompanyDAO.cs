using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITtechcom.Models.Entities;

namespace IITtechcom.Models.DAO
{
    public class CompanyDAO
    {
        private readonly ModelDbContext db = DataProvider.GetInstance();
        public Information GetInformation()
        {
            return db.Information.FirstOrDefault();
        }

        public bool AddCompanyIn4(Information in4)
        {
            try
            {
                db.Information.Add(in4);
                db.SaveChanges();

                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool UpdateIn4(Information in4)
        {
            try
            {
                var x = db.Information.SingleOrDefault(m => m.ID == in4.ID);

                x.NameCompany = in4.NameCompany;
                x.Logo = in4.Logo;
                x.Email = in4.Email;
                x.Address = in4.Address;
                x.Tel = in4.Tel;
                x.Hotline = in4.Hotline;
                x.Terms_Conditions = in4.Terms_Conditions;
                x.Decription = in4.Decription;
                x.MetaTitle = in4.MetaTitle;
                x.MetaDecription = in4.MetaDecription;

                db.SaveChanges();

                return  true;
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}