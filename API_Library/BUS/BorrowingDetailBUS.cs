using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Library.Models;
using API_Library.DAO;

namespace API_Library.BUS
{
    public class BorrowingDetailBUS : BorrowingDetailDAO
    {
        CopyDAO CopyDAO = new CopyDAO();
        public bool CreateList(List<BorrowingDetail> list, int borrowingId)
        {
            bool status;
            Copy copy;
            for(int i =0; i < list.Count; i++)
            {
                list[i].BorrowingId = borrowingId;
                status = this.Create(list[i]);
                if (!status) return false;
                copy = CopyDAO.GetById((int)list[i].CopyId);
                copy.BorrowStatus = 1;
                status = CopyDAO.Update(copy);
                if (!status) return false;
            }
            return true;
        }
        public bool UpdateDetail(BorrowingDetail detail)
        {
            bool status;
            Copy copy;
            status = Update(detail);
            if (!status) return false;
            if (detail.BorrowStatus == 2)
            {
                copy = CopyDAO.GetById((int)detail.CopyId);
                copy.BorrowStatus = 0;
                status = CopyDAO.Update(copy);
                if (!status) return false;
            }
            if(detail.BorrowStatus == 3)
            {
                copy = CopyDAO.GetById((int)detail.CopyId);
                copy.Status = false;
                copy.BorrowStatus = 0;
                status = CopyDAO.Update(copy);
                if (!status) return false;
            }
            return true;
        }
    }
}
