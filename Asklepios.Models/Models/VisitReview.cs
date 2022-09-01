using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asklepios.Core.Models
{
    public class VisitReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        public float GeneralRate { get; set; }
        public float CompetenceRate { get; set; }
        public float AtmosphereRate { get; set; }
        public string ShortDescription { get; set; }
        public DateTimeOffset ReviewDate { get; set; }
        public long RevieweeId { get; set; }
        public MedicalWorker Reviewee { get; set; }
        public long ReviewerId { get; set; }
        public Patient Reviewer { get; set; }
        public long VisitId { get; set; }
        public Visit Visit { get; set; }

        public  VisitReview MockClone( long id)
        {
            VisitReview review = new VisitReview
            {
                AtmosphereRate = this.AtmosphereRate,
                CompetenceRate = this.CompetenceRate,
                GeneralRate = this.GeneralRate,
                ShortDescription = this.ShortDescription,
                Id = id
            };

            return review;
        }
    }
}