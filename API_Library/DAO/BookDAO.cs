using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class BookDAO
    {
        libraryContext db = new libraryContext();
        CopyDAO copy = new CopyDAO();
        public BookDAO() { }
        public List<Book> Get()
        {
            return db.Books.Where(e => e.Status==true).ToList();
        }
        public List<Book> GetByCategoryId(int id)
        {
            return db.Books.Where(e=> e.CategoryId==id && e.Status==true).ToList();
        }
        public Book GetById(int id)
        {
            return db.Books.Where(e => e.BookId == id && e.Status == true).ToList().First();
        }
        public bool Create(Book o)
        {
            int id = db.Books.ToList().Last().BookId;
            id = id + 1;
            o.BookId = id;
            if(o.Title!="")
            {
                db.Books.Add(o);
                db.SaveChanges();
                return true;
            }    
            return false;
        }
        public bool Update(Book o)
        {
            Book obj = GetById(o.BookId);
            obj.Author = o.Author;
            obj.Title = o.Title;
            obj.Description = o.Description;
            obj.CategoryId = o.CategoryId;
            obj.Status = o.Status;
            obj.Publisher = o.Publisher;
            obj.LanguageId = o.LanguageId;
            obj.PositionId= o.PositionId;
            obj.PageNumber = o.PageNumber;
            obj.Price= o.Price;
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            Book obj = GetById(id);
            copy.DeleteByBookId(obj.BookId);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }
        public bool DeleteByCategoryId(int id)
        {
            List<Book> list = GetByCategoryId(id);
            for(int i =0;i<list.Count;i++)
            {
                list[i].Status = false;
            }
            db.SaveChanges();
            return true;
        }
    }
}
