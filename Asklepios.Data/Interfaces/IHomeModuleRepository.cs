using Asklepios.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.Interfaces
{
    public interface IHomeModuleRepository
    {
        IEnumerable<Location> GetAllLocations();
        Location GetLocationById(long locationId);
    }
}
