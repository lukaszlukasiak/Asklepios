using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class LocationsViewModel:BaseViewModel
    {
        public List<Asklepios.Core.Models.Location> Locations { get; set; }
        public LocationsViewModel(Patient patient, List<Asklepios.Core.Models.Location> locations)
        {
            SelectedPatient = patient;
            Locations = locations;
        }
    }
}
