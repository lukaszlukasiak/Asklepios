using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.DBContexts
{
    public class HomeDbContext : DbContext, IHomeModuleRepository
    {
        public HomeDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public IEnumerable<Location> GetAllLocations()
        {
            throw new NotImplementedException();
        }

        public Location GetLocationById(long locationId)
        {
            throw new NotImplementedException();
        }

        public Patient GetUserById(string userId)
        {
            throw new NotImplementedException();
        }

        public User LogIn(User user)
        {
            throw new NotImplementedException();
        }
    }
}
