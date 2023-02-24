using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asklepios.Core.Enums;

namespace Asklepios.Data.InMemoryContexts
{
    public class HomeInMemoryContext : IHomeModuleRepository
    {
        private List<Location> _locations;
        public HomeInMemoryContext()
        {
            if (!PatientMockDB.IsCreated)
            {
                PatientMockDB.SetDataHome();
            }
            //PatientMockDB.GetMedicalServices()
            _locations = PatientMockDB.GetAllLocations().ToList();

        }
        public List<Location> GetAllLocations()
        {
            return _locations;
        }

        public Location GetLocationById(long locationId)
        {
            if (_locations.Where(c=>c.Id==locationId).Count()>0)
            {
                return _locations.Where(c => c.Id == locationId).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public List<MedicalService> GetLocationServices(long id)
        {
            throw new NotImplementedException();
        }

        //public Patient GetUserById(long userId)
        //{
        //    throw new NotImplementedException();
        //}

        public User CheckEmailAndRole(User user)
        {
            List<User> users = PatientMockDB.Users;
            users = users.Where(c => c.UserType == user.UserType)?.Where(d=>d.WorkerModuleType==user.WorkerModuleType).ToList();
            string emailAddressUpper = user.Email.ToUpper();
            User user1 = users.Where(c => c.Email.ToUpper() == emailAddressUpper).FirstOrDefault();
            if (user1==null)
            {
                return null;
            }
            if (user.PasswordHash == user1.PasswordHash)
            {
                return user1;
            }
            else
            {
                return null;
            }
            //return PatientMockDB.Users.Where(c => c.UserType == user.UserType && c.UserName == user.UserName && c.Password == user.Password).FirstOrDefault();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public User CheckUserNameAndRole(string userName, WorkerModuleType workerModuleType, UserType userType)
        {
            throw new NotImplementedException();
        }
    }
}
