using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.HomeArea.Models
{
    public class ContactMessageViewModel
    {
        public string ContactName { get; set; }
        public string ContactEMailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string AlertMessage { get; set; }
    }
}
