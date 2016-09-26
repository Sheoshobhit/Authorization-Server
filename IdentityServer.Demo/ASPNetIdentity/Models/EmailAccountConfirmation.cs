using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetIdentity.Models
{
    public class EmailAccountConfirmation
    {
        public string AccountConfirmationToken { get; set; }
        public string EmailAddress { get; set; }
    }
}