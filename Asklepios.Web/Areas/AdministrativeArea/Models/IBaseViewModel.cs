using Asklepios.Core.Models;
using Asklepios.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asklepios.Web.Models;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public interface IBaseViewModel
    {
        public string UserName { get; set; }
        public ViewMessage ViewMessage { get; set; }
        //public string Message { get; set; }
        //public AlertMessageType AlertMessageType { get; set; }
        public bool HasMessage
        {
            get
            {
                if (ViewMessage==null)
                {
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(ViewMessage.Message))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
