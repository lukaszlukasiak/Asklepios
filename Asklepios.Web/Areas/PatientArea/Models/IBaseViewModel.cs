using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public interface IBaseViewModel
    {
        //public Patient SelectedPatient { get; set; }
        public string UserName { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
