using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class StaffDAO
    {
        libraryContext db = new libraryContext();
        AccountDAO account = new AccountDAO();
        public StaffDAO() { }
        public List<staff> Get()
        {
            return db.staff.Where(e => e.Status == true).ToList();
        }
        public staff GetById(short id)
        {
            return db.staff.Where(e => e.StaffId == id && e.Status == true).ToList().First();
        }
        public bool Create(staff o)
        {
            short id = db.staff.ToList().Last().StaffId;
            id = id += 1;
            o.StaffId = id;
            if (o.Name != "")
            {
                db.staff.Add(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(staff o)
        {
            staff obj = GetById(o.StaffId);
            obj.Name = o.Name;
            obj.Status = o.Status;
            obj.Address = o.Address;
            obj.Email = o.Email;
            obj.Phone = o.Phone;
            obj.StartDay = o.StartDay;
            obj.Gender = o.Gender;
            obj.Position = o.Position;
            db.SaveChanges();
            return true;
        }
        public bool Delete(short id)
        {
            staff obj = GetById(id);
            account.DeleteByStaffId(obj.StaffId);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }
    }
}
