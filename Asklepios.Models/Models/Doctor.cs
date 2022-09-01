using Asklepios.Core.Enums;

namespace Asklepios.Core.Models
{
    public class Doctor : MedicalWorker
    {
        public override string ProfessionalTitle => "Lekarz";

        
        //public Doctor(Person person, string doctorNumber) :base(person)
        //{
        //    ProfessionalNumber = doctorNumber;
        //    MedicalWorkerType = Enums.MedicalWorkerType.Doctor;
        //    PersonId = person.Id;   
        //}
        public Doctor(long personId, string professionalNumber) : base(personId)
        {
            ProfessionalNumber = professionalNumber;
            MedicalWorkerType = Enums.MedicalWorkerType.Doctor;
            PersonId = personId;
        }
    }
}