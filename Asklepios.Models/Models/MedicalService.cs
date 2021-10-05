using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class MedicalService
    {
        public string Name { get; set; }
        public decimal StandardPrice { get; set; }
        public string Description { get; set; }
        public IEnumerable< MedicalService> SubServices { get; set; }
    }
}
