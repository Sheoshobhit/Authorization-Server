using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetIdentity.Models
{
    public class ForgotPassword
    {
        public string Email { get; set; }
        public string OriginatingApplicationCode { get; set; }
        public string Language { get; set; }
        public string ClientId { get; set; }
        public string Username { get; set; }
    }
}