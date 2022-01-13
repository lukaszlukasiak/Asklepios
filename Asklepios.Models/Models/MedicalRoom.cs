using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class MedicalRoom
    {
        public string Name { get; set; }
        public string Description
        {
            get
            {
                return "Pokój numer: " + Name;
            }
        }
        public short FloorNumber { get; set; }
        public MedicalRoomType MedicalRoomType {get;set;}
        public long Id { get; set; }
    }
}
