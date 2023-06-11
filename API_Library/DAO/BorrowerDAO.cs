using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class BorrowerDAO
    {
        libraryContext db = new libraryContext();
        public BorrowerDAO() { }
        public List<Borrower> Get()
        {
            return db.Borrowers.Where(e => e.Status ==true).ToList();
        }
        public Borrower GetById(int id)
        {
            return db.Borrowers.Where(e => e.BorrowerId == id && e.Status == true).ToList().First();
        }
        public bool Create(Borrower o)
        {
            int id = db.Borrowers.ToList().Last().BorrowerId;
            id = id + 1;
            o.BorrowerId =  id;
            if (o.Name.Length > 0)
            {
                db.Borrowers.Add(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Borrower o)
        {
            Borrower obj = GetById(o.BorrowerId);
            obj.BorrowerId = o.BorrowerId;
            obj.Status = o.Status;
            obj.Name = o.Name;
            obj.Email= o.Email;
            obj.Address = o.Address;
            obj.Gender = o.Gender;
            obj.Phone = o.Phone;
            obj.AccountBalance = o.AccountBalance;
            obj.StartDay = o.StartDay;
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            Borrower obj = GetById(id);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }
    }
}
