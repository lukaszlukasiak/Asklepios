using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class VisitCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<MedicalService> PrimaryMedicalServices {get;set;}
    }
}
