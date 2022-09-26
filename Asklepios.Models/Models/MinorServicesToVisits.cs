using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public  class MedicalServiceToMedicalWorker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }

        public long? MedicalServiceId { get; set; }
        public MedicalService MedicalService { get; set; }

        public long? MedicalWorkerId { get; set; }
        public MedicalWorker MedicalWorker { get; set; }

    }
}
