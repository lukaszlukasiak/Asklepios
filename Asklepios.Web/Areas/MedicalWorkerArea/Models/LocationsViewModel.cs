using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class LocationsViewModel: IBaseViewModel
    {
        public List<Asklepios.Core.Models.Location> Locations { get; set; }
        public Patient SelectedPatient { get; set; }
        public string UserName { get; set; }

        public LocationsViewModel( List<Asklepios.Core.Models.Location> locations)
        {
            Locations = locations;
        }
    }
}
