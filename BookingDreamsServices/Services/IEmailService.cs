using BookingDreamsServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingDreamsServices.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
