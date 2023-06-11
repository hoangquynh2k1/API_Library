using API_Library.DAO;
using API_Library.Entities;
using API_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Library.BUS
{
    public class BorrowingBUS: BorrowingDAO
    {
        BorrowerDAO borrowerDAO = new BorrowerDAO();
        BorrowingDetailBUS DetailBUS = new BorrowingDetailBUS();

        public bool SendEmail(int borrowingID)
        {
            return true;
        }

        public List<BorrowingEntity> GetData()
        {
            List<Borrowing> borrowing = Get();
            List<BorrowingEntity> list = new List<BorrowingEntity>();
            for(int i =0;i<borrowing.Count;i++)
            {
                Borrower borrower = borrowerDAO.GetById((int)borrowing[i].BorrowerId);
                List<BorrowingDetail> listDetail = DetailBUS.GetByBorrowingId(
                    (int)borrowing[i].BorrowingId);
                BorrowingEntity entity = new BorrowingEntity();
                entity.BorrowerId = borrower.BorrowerId;
                entity.BorrowingId = borrowing[i].BorrowingId;
                entity.BorrowedDate = borrowing[i].BorrowedDate;
                entity.StaffId = borrowing[i].StaffId;
                entity.AppointmentDate = borrowing[i].AppointmentDate;
                entity.Status = borrowing[i].Status;
                entity.Details = listDetail;
                entity.Name = borrower.Name;
                entity.NotificationStatus= borrowing[i].NotificationStatus;
                entity.BorrowStatus = CheckBorrow(entity);
                list.Add(entity);
            }
            return list;
        }
        public List<BorrowingEntity> GetOverdue()
        {
            List<Borrowing> borrowing = Get();
            List<BorrowingEntity> list = new List<BorrowingEntity>();
            for (int i = 0; i < borrowing.Count; i++)
            {
                Borrower borrower = borrowerDAO.GetById((int)borrowing[i].BorrowerId);
                List<BorrowingDetail> listDetail = DetailBUS.GetByBorrowingId(
                    (int)borrowing[i].BorrowingId);
                BorrowingEntity entity = new BorrowingEntity();
                entity.BorrowerId = borrower.BorrowerId;
                entity.BorrowingId = borrowing[i].BorrowingId;
                entity.BorrowedDate = borrowing[i].BorrowedDate;
                entity.StaffId = borrowing[i].StaffId;
                entity.AppointmentDate = borrowing[i].AppointmentDate;
                entity.Status = borrowing[i].Status;
                entity.Details = listDetail;
                entity.Name = borrower.Name;
                entity.NotificationStatus = borrowing[i].NotificationStatus;
                entity.BorrowStatus = CheckBorrow(entity);
                if(entity.BorrowStatus == false)
                    entity.Overdue = CheckNearOverdue(entity);
                if(entity.Overdue == 1 || entity.Overdue == 2)
                {
                    list.Add(entity);
                }    
            }
            return list;
        }

        public bool Create(BorrowingEntity entity)
        {
            Borrowing borrowing = new Borrowing();
            borrowing.AppointmentDate = entity.AppointmentDate;
            borrowing.Status = entity.Status;
            borrowing.BorrowerId = entity.BorrowerId;
            borrowing.BorrowedDate = entity.BorrowedDate;
            borrowing.NotificationStatus = entity.NotificationStatus;
            borrowing.StaffId= entity.StaffId;
            bool status = this.Create(borrowing);
            Borrowing newBorrowing = Get().Where(x => (x.BorrowerId == borrowing.BorrowerId && 
            x.BorrowedDate == borrowing.BorrowedDate)).FirstOrDefault();
            if(newBorrowing != null)
            {
                DetailBUS.CreateList(entity.Details, newBorrowing.BorrowingId);
            }
            return true;
        }

        private bool CheckBorrow(BorrowingEntity e)
        {
            for(int i =0;i< e.Details.Count;i++)
            {
                if (e.Details[i].ReturnDate == null)
                    return false;
            }
            return true;
        }
        private int CheckNearOverdue(BorrowingEntity e)
        {
            if(e.AppointmentDate >= DateTime.Now && e.AppointmentDate <= DateTime.Now.AddDays(3)) return 1;
            else if (e.AppointmentDate < DateTime.Now) return 2;
            return 0;
        }
    }
}
