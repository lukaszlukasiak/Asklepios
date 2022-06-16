using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class MedicalService
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal StandardPrice { get; set; }
        public string Description { get; set; }
        public bool IsPrimaryService { get; set; }
        public bool RequireRefferal { get; set; }
        public IList< MedicalService> SubServices { get; set; }
    }
}
