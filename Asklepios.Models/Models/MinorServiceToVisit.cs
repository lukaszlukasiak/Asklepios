﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public  class MinorServiceToVisit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }
        public long? MedicalServiceId { get; set; }
        public MedicalService MedicalService { get; set; }

        public long? VisitId { get; set; }
        public Visit Visit { get; set; }

    }
}