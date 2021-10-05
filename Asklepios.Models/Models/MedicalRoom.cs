using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class MedicalRoom
    {
        public string Name { get; set; }
        public short FloorNumber { get; set; }
        public MedicalRoomType MedicalRoomType {get;set;}

    }
}
