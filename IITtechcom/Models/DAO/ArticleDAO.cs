using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITtechcom.Models.Entities;
using PagedList;

namespace IITtechcom.Models.DAO
{
    public class ArticleDAO
    {
        private readonly ModelDbContext db = DataProvider.GetInstance();

        public IEnumerable<Article> ListPaging(int? id, string searchString, int page, int pageSize)
        {
            IQueryable<Article> model = db.Articles;

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            if (id == null)
            {
                return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
            }
            else
            {
                model = model.Where(x => x.MenuID == id);

                var menus = db.Menus.Where(x => x.ParentID == id);
                foreach (var item in menus)
                {
                    IQueryable<Article> article = db.Articles.Where(x => x.MenuID == item.ID);
                    model = model.Concat(article);
                }


                return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
            }
        }

        public IEnumerable<Article> ListPagingHome(int? id, string searchString, int page, int pageSize)
        {
            IQueryable<Article> model = db.Articles.Where(x => x.ShowHome == true);

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            if (id == null)
            {
                return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
            }
            else
            {
                model = model.Where(x => x.MenuID == id);

                var menus = db.Menus.Where(x => x.ParentID == id);
                foreach (var item in menus)
                {
                    IQueryable<Article> article = db.Articles.Where(x => x.MenuID == item.ID && x.ShowHome == true);
                    model = model.Concat(article);
                }


                return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
            }
        }

        public bool AddArticle(Article article)
        {
            try
            {
                article.CreateDate = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Article GetArticle(int id)
        {
            return db.Articles.SingleOrDefault(x => x.ID == id);
        }

        public Article GetArticle(int? id)
        {
            if (id == null)
            {
                return db.Articles.OrderByDescending(x=>x.CreateDate).FirstOrDefault();
            }
            return db.Articles.SingleOrDefault(x => x.ID == id);
        }

        public bool UpdateArticle(Article article)
        {
            try
            {
                var x = db.Articles.SingleOrDefault(m => m.ID == article.ID);
                if (x != null)
                {
                    x.Name = article.Name;
                    x.Image = article.Image;
                    x.MenuID = article.MenuID;
                    x.CreateDate = article.CreateDate;
                    x.Content = article.Content;
                    x.Status = article.Status;
                    x.IsActive = article.IsActive;
                    x.ShowHome = article.ShowHome;
                    x.Decription = article.Decription;
                    x.MetaTitle = article.MetaTitle;
                    x.MetaDecription = article.MetaDecription;

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception )
            {
                return false;
            }

        }

        public bool DeleteArticle(int id)
        {
            try
            {
                var x = db.Articles.Find(id);
                db.Articles.Remove(x);
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