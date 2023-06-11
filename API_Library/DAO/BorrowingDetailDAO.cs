using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class BorrowingDetailDAO
    {
        libraryContext db = new libraryContext();
        public BorrowingDetailDAO() { }
        public List<BorrowingDetail> Get()
        {
            return db.BorrowingDetails.Where(e => e.Status == true).ToList();
        }
        public BorrowingDetail GetById(int id)
        {
            return db.BorrowingDetails.Where(e => e.BorrowingDetailId == id && e.Status == true).ToList().First();
        }
        public List<BorrowingDetail> GetByBorrowingId(int id)
        {
            return db.BorrowingDetails.Where(e => e.BorrowingId == id && e.Status == true).ToList();
        }

        public bool Create(BorrowingDetail o)
        {
            int id = db.BorrowingDetails.ToList().Last().BorrowingDetailId;
            id = id += 1;

            o.BorrowingDetailId = id;
            if (o.BorrowingDetailId > 0)
            {
                db.BorrowingDetails.Add(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(BorrowingDetail o)
        {   
            BorrowingDetail obj = GetById(o.BorrowingDetailId);
            obj.BorrowingId = o.BorrowingId;
            obj.CopyId = o.CopyId;
            obj.Status = o.Status;
            obj.ReturnDate= o.ReturnDate;
            obj.Durability= o.Durability;
            obj.Description= o.Description;
            obj.BorrowStatus= o.BorrowStatus;
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            BorrowingDetail obj = GetById(id);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }

    }
}
