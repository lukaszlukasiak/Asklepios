using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asklepios.Core.Models
{
    public class MedicalService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal StandardPrice { get; set; }
        public string Description { get; set; }
        public bool IsPrimaryService { get; set; }
        public bool RequireRefferal { get; set; }
        public long? PrimaryServiceId { get; set; }
        public virtual MedicalService PrimaryService { get; set; }
        public long? VisitCategoryId { get; set; }
        public virtual VisitCategory VisitCategory { get; set; }

        public virtual List< MedicalService> SubServices { get; set; }
        public virtual List<MedicalWorker> MedicalWorkers { get; set; }
        //public  virtual List<MedicalServiceToMedicalWorker> MedicalServiceMedicalWorker { get; set; }
        public virtual List<Location> Locations { get; set; }
        //public virtual List<Visit> Visits { get; set; }
        public virtual List<MinorServiceToVisit> MinorServicesToVisit { get; set; }

    }
}
