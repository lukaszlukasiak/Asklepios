﻿using Asklepios.Core.Models;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class MedicalWorkerViewModel:IBaseViewModel
    {
        public MedicalWorkerViewModel(MedicalWorker worker)
        {
            MedicalWorker = worker;
        }

        public MedicalWorker MedicalWorker { get; set; }
        public string UserName { get; set; }
        public ViewMessage ViewMessage { get; set; } = new ViewMessage();
        //public string Message { get; set; }
        //public AlertMessageType AlertMessageType { get; set; }
    }
}
