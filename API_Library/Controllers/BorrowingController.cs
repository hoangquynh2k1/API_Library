using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API_Library.BUS;
using API_Library.Models;
using System.Linq;
using System;
using API_Library.Entities;
using API_Library.Service;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Library.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        BorrowingBUS db = new BorrowingBUS();
        BorrowerBUS borrowerBUS = new BorrowerBUS();
        ISendMailService service;
        public BorrowingController(ISendMailService mailService) 
        {
            service= mailService;
        }
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<Borrowing> Get()
        {
            return db.Get();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public BorrowingEntity Get(int id)
        {
            List<BorrowingEntity> list = db.GetData();
            BorrowingEntity borrowing = list.Where(x => x.BorrowerId== id).FirstOrDefault();
            return borrowing;
        }

        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                int? dropdownId = null;
                string loc = "";
                if (formData.Keys.Contains("loc") && !string.IsNullOrEmpty(Convert.ToString(formData["loc"])))
                { loc = formData["loc"].ToString(); }
                if (formData.Keys.Contains("dropdown") && !string.IsNullOrEmpty(Convert.ToString(formData["dropdown"])))
                { dropdownId = int.Parse(formData["dropdown"].ToString()); }
                List<BorrowingEntity> list = db.GetData().Where(x => x.Name.Contains(loc)).ToList();
                switch(dropdownId)
                {
                    case 0:
                        break;
                    case 1:
                        list = list.Where(x => x.BorrowStatus == false).ToList(); break;
                    case 2:
                        list = list.Where(x => x.BorrowStatus == true).ToList(); break;
                    case 3:
                        list = list.Where(x => x.Status == true).ToList();
                        break;
                    case 4:
                        list = list.Where(x => x.Status == false).ToList();
                        break;
                }    
                long total = list.Count();
                list = list.
                    Skip(pageSize * (page - 1)).OrderBy(x => x.BorrowStatus).Take(pageSize).ToList();
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

        [HttpPost]
        public async Task<bool> SendEMail(BorrowingEntity entity)
        {
            Borrower borrower = borrowerBUS.GetById((int)entity.BorrowerId);
            string content = "<h1>Thông báp sắp đến hạn trả sách</h1>" +
                "<p>Thân gửi " + entity.Name + "" + "Bạn có đang mượn sách của thư viện BKE" +
                " và sắp tới hạn trả sách ngày" + entity.AppointmentDate + "." +
                "Mong bạn sẽ trả sách đúng hạn!</p>"; ;
            await service.SendEmailAsync(borrower.Email, "Thông báp sắp đến hạn trả sách", content);
            entity.NotificationStatus = true;
            Borrowing borrowing = new Borrowing();
            borrowing.BorrowerId = entity.BorrowerId;
            borrowing.BorrowingId = entity.BorrowingId;
            borrowing.BorrowedDate = entity.BorrowedDate;
            borrowing.AppointmentDate = entity.AppointmentDate;
            borrowing.StaffId = entity.StaffId;
            borrowing.NotificationStatus = entity.NotificationStatus;
            borrowing.Status = true;
            bool status = Put(borrowing);
            return status;
        }


        [HttpPost]
        public IActionResult CheckOverdue([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                int? dropdownId = null;
                if (formData.Keys.Contains("dropdown") && !string.IsNullOrEmpty(Convert.ToString(formData["dropdown"])))
                { dropdownId = int.Parse(formData["dropdown"].ToString()); }
                List<BorrowingEntity> list = db.GetOverdue();
                list = list.Where(x => x.BorrowStatus == false).ToList();
                long total = list.Count();
                list = list.
                    Skip(pageSize * (page - 1)).OrderBy(x => x.AppointmentDate).Take(pageSize).ToList();
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

        // POST api/<AccountController>
        [HttpPost]
        public bool Post(BorrowingEntity o)
        {
            return db.Create(o);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public bool Put(Borrowing o)
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
