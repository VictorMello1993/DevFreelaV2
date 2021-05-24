using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Domain
{
    public class EmailConfiguration
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string ServerMailAddress { get; set; }
        public string ServerMailPort { get; set; }
        public bool UseSSL { get; set; }
    }
}
