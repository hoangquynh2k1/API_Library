using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Library.BUS;
using API_Library.Models;
using API_Library.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Library.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        LoginBUS db = new LoginBUS();
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<Login> Get()
        {
            return db.Get();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public Login Get(int id)
        {
            return db.GetById(id);
        }

        [HttpGet]
        public AccountEntity CheckLogin(string id, string password)
        {
            return db.CheckLogin(id, password);
        }

        // POST api/<AccountController>
        [HttpPost]
        public bool Post(Login o)
        {
            return db.Create(o);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public bool Put(Login o)
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
