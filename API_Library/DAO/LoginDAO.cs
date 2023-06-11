using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class LoginDAO
    {
        libraryContext db = new libraryContext();
        public LoginDAO() { }
        public List<Login> Get()
        {
            return db.Logins.Where(e => e.Status == true).ToList();
        }
        public Login GetById(int id)
        {
            return db.Logins.Where(e => e.LoginId == id && e.Status == true).ToList().First();
        }
        public bool Create(Login o)
        {
            int id = db.Logins.ToList().Last().LoginId;
            id = id += 1;
            o.LoginId = id;
            if (o.AccountId != 0)
            {
                db.Logins.Add(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Login o)
        {
            Login obj = GetById(o.LoginId);
            obj.AccountId = o.AccountId;
            obj.LoginDate= o.LoginDate;
            obj.Status = o.Status;
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            Login obj = GetById(id);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }
    }
}
