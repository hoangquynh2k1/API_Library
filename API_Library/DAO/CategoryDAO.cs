using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class CategoryDAO
    {
        libraryContext db = new libraryContext();
        BookDAO Book = new BookDAO();
        public List<Category> Get()
        {
            return db.Categories.Where(e => e.Status == true).ToList();
        }
        public Category GetById(short id)
        {
            return db.Categories.Where(e => e.CategoryId == id && e.Status == true).ToList().First();
        }
        public bool Create(Category o)
        {
            short id = (db.Categories.ToList().Last().CategoryId);
            id = id+=1;
            o.CategoryId = id;
            if (o.Name != "")
            {
                db.Categories.Add(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Category o)
        {
            Category obj = GetById(o.CategoryId);
            obj.Name = o.Name;
            obj.Status = o.Status;
            db.SaveChanges();
            return true;
        }
        public bool Delete(short id)
        {
            Category obj = GetById(id);
            Book.DeleteByCategoryId(obj.CategoryId);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }
    }
}
