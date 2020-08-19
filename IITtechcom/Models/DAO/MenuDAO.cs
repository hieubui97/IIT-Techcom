using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITtechcom.Models.Entities;

namespace IITtechcom.Models.DAO
{
    public class MenuDAO
    {
        private readonly ModelDbContext db = DataProvider.GetInstance();

        public List<Menu> GetMenus(int? id)
        {
            if (id == null)
            {
                return db.Menus.ToList();
            }

            return db.Menus.Where(x => x.ParentID == id).ToList();
        }

        public Menu GetMenu(int id)
        {
            return db.Menus.Find(id);
        }

        public List<Menu> GeList(int? id)
        {
            var menus = new List<Menu>();
            var menu = new Menu() { Name = null };
            menus.Add(menu);

            if (id != null)
            {
                foreach (var item in db.Menus.Where(x => x.ParentID != id && x.ID != id).ToList())
                {
                    menus.Add(item);
                }
            }
            else
            {
                foreach (var item in db.Menus.ToList())
                {
                    menus.Add(item);
                }
            }


            return menus;
        }

        public List<Menu> GeList2()
        {
            return db.Menus.ToList();
        }

        public bool AddMenu(Menu menu)
        {
            try
            {
                var parentMenu = db.Menus.SingleOrDefault(x => x.ID == menu.ParentID);
                if (parentMenu == null || parentMenu.ID == menu.ID)
                {
                    menu.Level = 1;
                    menu.ParentID = null;
                }
                else
                {
                    menu.Level = parentMenu.Level + 1;
                }

                db.Menus.Add(menu);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMenu(Menu menu)
        {
            try
            {
                var x = db.Menus.Find(menu.ID);
                x.Name = menu.Name;

                x.Decription = menu.Decription;
                x.IsActive = menu.IsActive;
                x.Status = menu.Status;
                x.Location = menu.Location;
                x.MetaTitle = menu.MetaTitle;
                x.MetaDecription = menu.MetaDecription;

                var parentMenu = db.Menus.SingleOrDefault(m => m.ID == menu.ParentID);
                if (parentMenu == null || parentMenu.ID == menu.ID)
                {
                    x.Level = 1;
                    x.ParentID = null;
                }
                else
                {
                    x.Level = parentMenu.Level + 1;
                    x.ParentID = menu.ParentID;
                }

                int level = 1;
                if (x.Level != null)
                {
                    level = (int)x.Level + 1;
                }

                SetLevel(menu.ID, level);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetLevel(int menuId, int level)
        {

            var childMenu = db.Menus.Where(m => m.ParentID == menuId).ToList();

            foreach (var item in childMenu)
            {
                item.Level = level;
                db.SaveChanges();

                // gọi đệ quy để set level hết các menu con
                var level2 = level + 1;
                SetLevel(item.ID, level2);
            }
        }



        public bool DeleteMenu(int id)
        {
            try
            {
                var x = db.Menus.FirstOrDefault(m => m.ID == id);
                db.Menus.Remove(x);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}