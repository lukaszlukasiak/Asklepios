using Asklepios.Core.Models;
using Asklepios.Data.InMemoryContexts;

namespace TestConsole
{
    public  class Progam
    {
        public static void Main(string[] args)
        {
            PatientMockDB.SetData();
            List<Patient> patients = PatientMockDB.AllPatients;
        }
    }
}
