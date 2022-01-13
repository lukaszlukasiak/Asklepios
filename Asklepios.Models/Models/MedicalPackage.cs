using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class MedicalPackage
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public string Description { get; set; }
        public Dictionary<MedicalService, decimal> ServicesDiscounts { get; set; }
    }
}
