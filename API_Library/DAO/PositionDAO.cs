using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;

namespace API_Library.DAO
{
    public class PositionDAO
    {
        libraryContext db = new libraryContext();
        public PositionDAO() { }
        public List<Position> Get()
        {
            return db.Positions.Where(e => e.Status == true).ToList();
        }
        public Position GetById(short id)
        {
            return db.Positions.Where(e => e.PositionId == id && e.Status == true).ToList().First();
        }
        public bool Create(Position o)
        {
            short id = db.Positions.ToList().Last().PositionId;
            id = id += 1;
            o.PositionId = id;
            if (o.Floor > 0)
            {
                db.Positions.Add(o);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Position o)
        {
            Position obj = GetById(o.PositionId);
            obj.Shelf = o.Shelf;
            obj.Floor= o.Floor;
            obj.Status = o.Status;
            db.SaveChanges();
            return true;
        }
        public bool Delete(short id)
        {
            Position obj = GetById(id);
            obj.Status = false;
            db.SaveChanges();
            return true;
        }
    }
}
