//using PdfSharp.Pdf;

using PdfSharpCore.Pdf;
using System;

namespace Asklepios.Core.Models
{
    public  class MedicalTestResult
    {
        public long Id { get; set; }
        public string Descritpion { get; set; }
        public MedicalService MedicalService { get; set; }
        public byte[] PdfDocument { get; set; }
        //public VisitSummary VisitSummary { get; set; }
        public MedicalWorker MedicalWorker { get; set; }
        public DateTimeOffset Date { get; set; }
        public Patient Patient { get; set; }
        public Visit _visit;
        public Visit Visit
        {
            get
            {
                return _visit;
            }
            set
            {
                _visit = value;
                MedicalWorker = value.MedicalWorker;
                Patient = value.Patient;
            }
        }

        public MedicalTestResult()
        {

        }
    }
}