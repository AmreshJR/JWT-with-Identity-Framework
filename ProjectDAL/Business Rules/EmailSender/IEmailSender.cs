using ProjectDAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Message Message);
        Task SendEmailAsync(Message Message);
    }
}
