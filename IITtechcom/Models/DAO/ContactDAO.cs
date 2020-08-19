using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITtechcom.Models.Entities;
using PagedList;

namespace IITtechcom.Models.DAO
{
    public class ContactDAO
    {
        private readonly ModelDbContext db = new ModelDbContext();

        public IEnumerable<Contact> ListPaging(int page, int pageSize, string searchString)
        {
            IQueryable<Contact> model = db.Contacts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Email.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public bool DeleteContact(int id)
        {
            try
            {
                var x = db.Contacts.FirstOrDefault(m=>m.ID == id);
                db.Contacts.Remove(x);
                db.SaveChanges();

                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}