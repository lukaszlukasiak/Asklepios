namespace Asklepios.Core.Models
{
    public class IssuedMedicine
    {
        public long Id { get; set; }
        public string MedicineName { get; set; }
        public string PackageSize { get; set; }
        //public string Dosage { get; set; }
        public float PaymentDiscount { get; set; }
    }
}