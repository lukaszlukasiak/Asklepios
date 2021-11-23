//using PdfSharp.Pdf;

using PdfSharpCore.Pdf;

namespace Asklepios.Core.Models
{
    public  class MedicalServiceOutcome
    {
        public string Descritpion { get; set; }
        public PdfDocument PdfDocument { get; set; }
    }
}