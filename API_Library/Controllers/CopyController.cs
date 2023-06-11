using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Library.BUS;
using API_Library.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Library.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CopyController : ControllerBase
    {
        CopyBUS db = new CopyBUS();
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<Copy> Get()
        {
            return db.Get();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public Copy Get(int id)
        {
            return db.GetById(id);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public IEnumerable<Copy> GetByCategoryId(int id)
        {
            return db.GetByBookId(id);
        }

        // POST api/<AccountController>
        [HttpPost]
        public bool Post(Copy o)
        {
            return db.Create(o);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public bool Put(Copy o)
        {
            return db.Update(o);
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return db.Delete(id);
        }
    }
}
