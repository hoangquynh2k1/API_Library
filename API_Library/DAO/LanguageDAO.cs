using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class LanguageDAO
    {
        libraryContext db = new libraryContext();
        public LanguageDAO() { }
        public List<Language> Get()
        {
            return db.Languages.Where(e => e.Status == true).ToList();
        }
        public Language GetById(int id)
        {
            return db.Languages.Where(e => e.LanguageId == id && e.Status == true).ToList().First();
        }
        public bool Create(Language o)
        {
            short id = db.Languages.ToList().Last().LanguageId;
            id = id += 1;
            o.LanguageId = id;
            if (o.Name.Length > 0)
            {
                db.Languages.Add(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Language o)
        {
            Language obj = GetById(o.LanguageId);
            obj.Name = o.Name;
            obj.Description= o.Description;
            obj.Status= o.Status;
            db.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            Language obj = GetById(id);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }

    }
}
