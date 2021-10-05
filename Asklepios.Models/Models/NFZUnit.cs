using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class NFZUnit
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public VoivodeshipType Voivodeship { get; set; }
    }
}
