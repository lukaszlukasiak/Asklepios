using Asklepios.Core.Models;
using Asklepios.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class UserViewModel : IBaseViewModel
    {
        public string UserName { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public AlertMessageType AlertMessageType { get; set; }
    }
}
