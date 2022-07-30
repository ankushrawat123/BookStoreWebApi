using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Admin
{
    public class AdminResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
