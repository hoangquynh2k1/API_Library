using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class AccountDAO
    {
        libraryContext db = new libraryContext();
        public AccountDAO() { }
        public List<Account> Get()
        {
            return db.Accounts.Where(e => e.Status == true).ToList();
        }
        public Account GetById(int id)
        {
            return db.Accounts.Where(e => e.StaffId == id && e.Status == true).ToList().First();
        }
        public Account CheckLogin(string id, string pass)
        {
            Account account = db.Accounts.FirstOrDefault(x => x.StaffId == short.Parse(id) 
                && x.Password == pass && x.Status ==true);
            return account;
        }
        public List<Account> GetByStaffId(short id)
        {
            return db.Accounts.Where(e => e.StaffId == id && e.Status == true).ToList();
        }

        public bool Create(Account o)
        {
            int id = db.Accounts.ToList().Last().AccountId;
            id = id += 1;
            o.AccountId = id;
            if (o.StaffId != null)
            {
                db.Accounts.Add(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Account o)
        {
            Account obj = GetById(o.AccountId);
            obj.AccountId = o.AccountId;
            obj.StaffId = o.StaffId;
            obj.Password = o.Password;
            obj.Status = o.Status;
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            Account obj = GetById(id);
            //copy.DeleteByBookId(obj.BookId);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }
        public bool DeleteByStaffId(short id)
        {
            List<Account> list = GetByStaffId(id);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Status = false;
            }
            db.SaveChanges();
            return true;
        }
    }
}
