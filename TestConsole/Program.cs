using Asklepios.Core.Models;
using Asklepios.Data.InMemoryContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
