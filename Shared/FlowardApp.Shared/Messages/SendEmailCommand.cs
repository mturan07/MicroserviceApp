using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowardApp.Shared.Messages
{
    public class SendEmailCommand
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string EmailAddress { get; set; }
    }
}
