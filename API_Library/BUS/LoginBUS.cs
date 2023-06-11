using API_Library.Models;
using API_Library.DAO;
using API_Library.Entities;
using System;

namespace API_Library.BUS
{
    public class LoginBUS : LoginDAO
    {
        AccountDAO accountDAO = new AccountDAO();
        StaffDAO staffDAO = new StaffDAO();
        public AccountEntity CheckLogin(string id, string pass)
        {
            Account account = accountDAO.CheckLogin(id, pass);
            if(account == null)
            {
                return null;
            }
            else
            {
                staff staff = staffDAO.GetById((short)account.StaffId);
                AccountEntity entity = new AccountEntity();
                entity.StaffId = account.StaffId;
                entity.Position = staff.Position;
                entity.Name = staff.Name;
                Login login = new Login();
                login.LoginDate = DateTime.Now;
                login.Status = true;
                login.AccountId = account.AccountId;
                //Create(login);
                return entity;
            }
        }
    }
}
