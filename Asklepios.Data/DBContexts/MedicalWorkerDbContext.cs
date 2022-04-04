using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Asklepios.Data
{
    public class MedicalWorkerDbContext : DbContext, IMedicalWorkerModuleRepository
    {
        public List<Visit> GetFutureVisitsByMedicalWorkerId(long id)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetHistoricalVisitsByMedicalWorkerId(long id)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetLocations()
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerByUserId(long personId)
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerData()
        {
            throw new NotImplementedException();
        }

        public List<VisitReview> GetReviewsByMedicalWorkerId(long id)
        {
            throw new NotImplementedException();
        }

        public Visit GetVisitById(long currentVisitId)
        {
            throw new NotImplementedException();
        }
    }
}
