using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class ImageDAO
    {
        libraryContext db = new libraryContext();
        public ImageDAO() { }
        public List<Image> Get()
        {
            return db.Images.Where(e => e.Status == true).ToList();
        }
        public Image GetById(int id)
        {
            return db.Images.Where(e => e.ImageId == id && e.Status == true).ToList().First();
        }
        public List<Image> GetByBookId(int id)
        {
            return db.Images.Where(e => e.BookId == id && e.Status == true).ToList();
        }

        public bool Create(Image o)
        {
            int id = db.Images.ToList().Last().ImageId;
            id = id += 1;
            o.ImageId = id;
            if (o.ImageId != 0)
            {
                db.Images.Add(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Image o)
        {
            Image obj = GetById(o.ImageId);
            obj.BookId = o.BookId;
            obj.Path= o.Path;
            obj.Status = o.Status;
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            Image obj = GetById(id);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }

    }
}
