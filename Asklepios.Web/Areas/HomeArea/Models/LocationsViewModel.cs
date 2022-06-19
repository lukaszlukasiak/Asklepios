using Asklepios.Core.Models;
using Asklepios.Web.Areas.PatientArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.HomeArea.Models
{
    public class LocationsViewModel: IBaseViewModel
    {
        public List<Asklepios.Core.Models.Location> Locations { get; set; }
        public string UserName { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
