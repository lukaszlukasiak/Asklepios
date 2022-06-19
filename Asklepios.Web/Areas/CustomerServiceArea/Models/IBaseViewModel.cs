using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public interface IBaseViewModel
    {
        public Patient SelectedPatient { get; set; }
        public string UserName { get; set; }
    }
}
