using System;
using System.Collections.Generic;

namespace WebApiHarjoituskoodi.ViewModels
{
    public partial class LoginsWithToken
    {
        public int LoginId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int? AccesslevelId { get; set; }
        public string Token { get; set; }

    }
}
