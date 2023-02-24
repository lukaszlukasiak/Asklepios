using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.MockModels
{
    public  class ServiceMock
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public decimal StandardPrice { get; set; }
        public string Description { get; set; }
        public bool IsPrimaryService { get; set; }
        public bool RequireRefferal { get; set; }
        public long? PrimaryServiceId { get; set; }
        public long? VisitCategoryId { get; set; }

        public ServiceMock()
        {

        }

        public ServiceMock ( MedicalService medicalService)
        {
            Id = medicalService.Id;
            Description = medicalService.Description;
            IsPrimaryService = medicalService.IsPrimaryService;
            Name = medicalService.Name;
            PrimaryServiceId = medicalService.PrimaryServiceId;
            RequireRefferal = medicalService.RequireRefferal;
            StandardPrice = medicalService.StandardPrice;
            VisitCategoryId = medicalService.VisitCategoryId;
        }

    }
}
