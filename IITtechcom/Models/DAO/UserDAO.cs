using IITtechcom.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITtechcom.Common;
using PagedList;

namespace IITtechcom.Models.DAO
{
    public class UserDAO
    {
        private readonly ModelDbContext db = DataProvider.GetInstance();
        public bool Login(string UserName, string Password)
        {
            var result = db.Users.Count(x => x.UserName == UserName && x.Password == Password);

            if (result > 0)
                return true;
            else
                return false;
        }

        public User GetUser(string UserName, string Password)
        {
            var result = db.Users
                .SingleOrDefault(x => x.UserName == UserName && x.Password == Password);

            return result;
        }

        public List<User> GetAllUser()
        {
            var result = db.Users.ToList();

            return result;
        }

        public IEnumerable<User> ListPaging(int page, int pageSize, string searchString)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Email.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public bool AddUser(User user)
        {
            try
            {
                user.Password = Encryptor.MD5Hash(user.Password);
                user.CreateDate = DateTime.Now;
                user.IsEditor = true;

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public User GetUserById(int id)
        {
            return db.Users.SingleOrDefault(x => x.ID == id);
        }

        public bool UpdateUser(User user)
        {
            try
            {
                var x = db.Users.SingleOrDefault(m => m.ID == user.ID);
                if (x != null)
                {
                    x.UserName = user.UserName;
                    x.Password = Encryptor.MD5Hash(user.Password);
                    x.Name = user.Name;
                    x.Email = user.Email;
                    x.CreateDate = user.CreateDate;
                    x.IsAdmin = user.IsAdmin;
                    x.IsEditor = user.IsEditor;
                }

                db.SaveChanges();

                return true;
            }
            catch (Exception )
            {
                return false;
            }
            
        }

        public bool DeleteUser(int id)
        {
            try
            {
                var x = db.Users.SingleOrDefault(m=>m.ID == id);
                db.Users.Remove(x);
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