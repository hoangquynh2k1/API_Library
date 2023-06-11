using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class CopyDAO
    {
        libraryContext db = new libraryContext();
        public CopyDAO() { }
        public List<Copy> Get()
        {
            return db.Copies.Where(e => e.Status == true && e.BorrowStatus==0).ToList();
        }
        public List<Copy> GetByBookId(int id)
        {
            return db.Copies.Where(e => e.BookId == id && e.Status == true && e.BorrowStatus == 0).ToList();
        }
        public Copy GetById(int id)
        {
            return db.Copies.Where(e => e.CopyId == id && e.Status == true).ToList().FirstOrDefault();
        }
        public bool Create(Copy o)
        {
            int id = db.Copies.ToList().Last().CopyId;
            id = id += 1;
            o.CopyId = id;
            if (o.Durability > 0)
            {
                db.Copies.Add(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Copy o)
        {
            Copy obj = GetById(o.CopyId);
            obj.BookId = o.BookId;
            obj.BorrowStatus = o.BorrowStatus;
            obj.Durability = o.Durability;
            obj.Status = o.Status;
            obj.Description= o.Description;
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            Copy obj = GetById(id);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }
        public bool DeleteByBookId(int id)
        {
            List<Copy> list = GetByBookId(id);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Status = false;
            }
            db.SaveChanges();
            return true;
        }

    }
}
