using AspNetIdentity.Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetIdentity.Demo.Interfaces
{
    public interface IEmailProvider
    {
        void Send(EmailMessage message);
    }
}
