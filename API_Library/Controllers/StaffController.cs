using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Library.BUS;
using API_Library.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Library.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        StaffBUS db = new StaffBUS();
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<staff> Get()
        {
            return db.Get();
        }
        [HttpGet]
        public int Get111()
        {
            int z = 0;
            int[] a = new int[5] { 22, 7, 8, 3, 5 };
            z = a[0] - a[1];
            for (int i =0;i<a.Length-1;i++)
            {
                for(int j=i+1;j<a.Length;j++)
                {
                    int t = a[i] - a[j];
                    if(t<z&&t>0)
                        z = t;
                }    
            }
            return z;
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public staff Get(short id)
        {
            return db.GetById(id);
        }

        // POST api/<AccountController>
        [HttpPost]
        public bool Post(staff o)
        {
            return db.Create(o);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public bool Put(staff o)
        {
            return db.Update(o);
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public bool Delete(short id)
        {
            return db.Delete(id);
        }
    }
}
