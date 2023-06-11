﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Library.BUS;
using API_Library.Models;
using System.Linq;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Library.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryBUS db = new CategoryBUS();
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return db.Get();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public Category Get(short id)
        {
            return db.GetById(id);
        }

        // POST api/<AccountController>
        [HttpPost]
        public bool Post(Category o)
        {
            return db.Create(o);
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
                if (formData.Keys.Contains("categoryId") && !string.IsNullOrEmpty(Convert.ToString(formData["ma_danh_muc"])))
                { categoryId = int.Parse(formData["ma_sach"].ToString()); }
                List<Category> list = db.Get();
                long total = list.Count();
                list = list.Where(x => (x.Name.ToLower()).Contains(loc.ToLower())).
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


        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public bool Put(Category o)
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
