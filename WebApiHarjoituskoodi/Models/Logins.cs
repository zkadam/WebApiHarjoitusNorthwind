using System;
using System.Collections.Generic;

namespace WebApiHarjoituskoodi.Models
{
    public partial class Logins
    {
        public int LoginId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int? AccesslevelId { get; set; }
    }
}
