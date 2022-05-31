using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Account
    {
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
    }
}