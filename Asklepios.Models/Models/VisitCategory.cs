using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asklepios.Core.Models
{
    public class VisitCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        public string CategoryName { get; set; }
        public VisitCategoryType Type { get; set; }
        public List<MedicalService> MedicalServices {get;set;}
    }
}
