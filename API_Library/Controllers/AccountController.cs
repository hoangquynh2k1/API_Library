using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Library.BUS;
using API_Library.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Library.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AccountBUS db = new AccountBUS();
        LoginBUS login = new LoginBUS();
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return db.Get();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public Account Get(int id)
        {
            return db.GetById(id);
        }

        // POST api/<AccountController>
        [HttpPost]
        public bool Post(Account o)
        {
            return db.Create(o);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public bool Put(Account o)
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
