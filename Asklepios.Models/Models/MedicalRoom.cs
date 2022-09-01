using Asklepios.Core.Enums;
using Asklepios.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asklepios.Core.Models
{
    public class MedicalRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        [Display(Name = "Placówka")]
        public long LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę pokoju!")]
        [Display(Name = "Nazwa pokoju")]
        public string Name { get; set; }
        public string Description
        {
            get
            {
                return "Pokój numer: " + Name;
            }
        }
        public string LongDescription
        {
            get
            {
                if (MedicalRoomType.HasValue)
                {
                    return "Pokój numer: " + Name + " | " + MedicalRoomType.GetDescription();
                }
                else
                {
                    return "Pokój numer: " + Name;
                }
            }
        }
        [Required(ErrorMessage = "Wprowadź numer piętra!")]
        [Display(Name = "Numer piętra")]
        public short? FloorNumber { get; set; }
        [Required(ErrorMessage = "Wprowadź typ pokoju!")]
        [Display(Name = "Typ pokoju")]
        public MedicalRoomType? MedicalRoomType {get;set;}
        public bool IsValid 
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    if (FloorNumber.HasValue)
                    {
                        if (MedicalRoomType.HasValue)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}
