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
        List<Location> GetAllLocations();
        Location GetLocationById(long locationId);
        User LogIn(User user);
        //Patient GetUserById(long userId);
        List<MedicalService> GetLocationServices(long id);
    }
}
