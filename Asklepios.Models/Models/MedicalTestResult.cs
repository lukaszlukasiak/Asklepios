//using PdfSharp.Pdf;

using PdfSharpCore.Pdf;

namespace Asklepios.Core.Models
{
    public  class MedicalTestResult
    {
        public long Id { get; set; }
        public string Descritpion { get; set; }
        public MedicalService MedicalService { get; set; }
        public PdfDocument PdfDocument { get; set; }
        public VisitSummary VisitSummary { get; set; }
        public MedicalTestResult()
        {

        }
    }
}