using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Location
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public VoivodeshipType VoivodeshipType { get; set;}
        public string City { get; set; }
        public string StreetAndNumber { get; set; }
        public IEnumerable<string> Facilities { get; set; }
        public IEnumerable<string> Services { get; set; }
        public IEnumerable<MedicalRoom> MedicalRooms { get; set; }
    }
}
