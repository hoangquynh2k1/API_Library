using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Library.BUS;
using API_Library.Models;
using System.Linq;
using System;
using API_Library.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Library.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        BookBUS db = new BookBUS();
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return db.Get();
        }

        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return db.GetById(id);
        }

        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                int? categoryId = null;
                string loc = "";
                if (formData.Keys.Contains("loc") && !string.IsNullOrEmpty(Convert.ToString(formData["loc"]))) 
                { loc = formData["loc"].ToString(); }
                if (formData.Keys.Contains("categoryId") && !string.IsNullOrEmpty(Convert.ToString(formData["categoryId"])))
                { categoryId = int.Parse(formData["categoryId"].ToString()); }
                List<BookEntity> list = db.GetBookEntities();
                long total = list.Count();
                list = list.Where(x => ((x.CategoryId == categoryId || categoryId == null || categoryId == 0))).ToList();
                list = list.Where(x => x.Title.ToLower().Contains(loc.ToLower()) || x.Description.ToLower().Contains(loc.ToLower())).
                    Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                return Ok(
                           new DataSearch
                           {
                               page = page,
                               totalItem = total,
                               pageSize = pageSize,
                               data = list
                           }
                         );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public IEnumerable<Book> GetByCategoryId(int id)
        {
            return db.GetByCategoryId(id);
        }

        [HttpPost]
        public bool Post(Book o)
        {
            return db.Create(o);
        }

        [HttpPut("{id}")]
        public bool Put(Book o)
        {
            return db.Update(o);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return db.Delete(id);
        }
    }
}
