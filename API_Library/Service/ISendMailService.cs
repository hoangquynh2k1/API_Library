﻿using API_Library.Entities;
using System.Threading.Tasks;

namespace API_Library.Service
{
    public interface ISendMailService
    {
        Task SendMail(MailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
