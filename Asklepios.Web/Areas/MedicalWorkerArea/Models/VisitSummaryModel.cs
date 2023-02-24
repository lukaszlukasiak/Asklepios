using Asklepios.Core.MockModels;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class VisitSummaryModel
    {
        public List<RecommendationMock> Recommendations { get; set; }
        public RecommendationMock RecommendationToAdd { get; set; }
        //public RecommendationMock RecommendationToRemove { get; set; }
        public PrescriptionMock PrescriptionToAdd { get; set; }
        public MedicineMock MedicineToAdd { get; set; }
        public List<MedicineMock> Medicines { get; set; }
        public List<ServiceMock> MinorMedicalServices { get; set; }
        public TestResultMock TestResult { get; set; }
        public List<ReferralMock> ReferralMocks { get; set; }
        public ReferralMock ReferralMockToAdd { get; set; }
        [Required]
        [Display(Name = "Wywiad medyczny")]
        public string MedicalHistory { get; set; }

        internal Visit UpdateVisit(Visit visit)
        {
            if (Recommendations!=null && Recommendations.Count>0)
            {
                List<Recommendation> recommendations = Recommendations.Select(c=>c.RecommendationFromMock()).ToList();// (recomm => recomm.RecommendationFromMock())
                recommendations.ForEach(r=>r.VisitId = visit.Id);


                visit.Recommendations = recommendations;
            }
            if (PrescriptionToAdd != null)
            {
                Prescription prescription = PrescriptionToAdd.PrescriptionFromMock(Medicines);
                prescription.VisitId = visit.Id;
                prescription.IssuedById = visit.MedicalWorkerId;
                prescription.IssuedToId = visit.PatientId;
                prescription.IssueDate = DateTime.Now;
                visit.Prescription = prescription;
                //if (Medicines!=null && Medicines.Count>0)
                //{
                //    List<IssuedMedicine> medicines = Medicines.Select(c => new IssuedMedicine() { MedicineName = c.MedicineName, PackageSize = c.PackageSize, PaymentDiscount = c.PaymentDiscount });
                //    prescription.IssuedMedicines = medicines;
                //}
            }
            if (MinorMedicalServices!=null && MinorMedicalServices.Count>0)
            {
                List<MedicalService> services = MinorMedicalServices.Select(c => new MedicalService() { Id = c.Id }).ToList();
                visit.MinorMedicalServices = services;

            }
            if (TestResult!=null)
            {
                MedicalTestResult medicalTestResult = TestResult.TestResultFromMock();
                medicalTestResult.VisitId = visit.Id;
                medicalTestResult.MedicalWorkerId= visit.MedicalWorkerId;
                medicalTestResult.PatientId= visit.PatientId;
                visit.MedicalTestResult = medicalTestResult;
            }
            if (ReferralMocks!=null && ReferralMocks.Count>0)
            {
                List<MedicalReferral> medicalReferrals = ReferralMocks.Select(c => c.ReferralFromMock()).ToList();
                medicalReferrals.ForEach(c => c.IssuedById = visit.MedicalWorkerId);
                medicalReferrals.ForEach(c => c.IssuedToId = visit.PatientId);
                medicalReferrals.ForEach(c=> c.VisitWhenIssuedId= visit.Id);
                visit.ExaminationReferrals = medicalReferrals;
            }
            return visit;
        }
    }
}
