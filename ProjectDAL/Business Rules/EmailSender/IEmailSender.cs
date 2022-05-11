using ProjectDAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.EmailSender
{
    public interface IEmailSender
    {
        public void Send(string mail, string callback);
      /*  void SendEmail(Message Message);
        Task SendEmailAsync(Message Message);*/
    }
}
