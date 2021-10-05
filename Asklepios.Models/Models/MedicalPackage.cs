using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class MedicalPackage
    {
        public String Name { get; set; }
        public Dictionary<MedicalService, float> ServicesDiscounts { get; set; }
    }
}
