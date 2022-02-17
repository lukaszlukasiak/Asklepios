using Asklepios.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Location
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="Proszę wprowadzić nazwę placówki")]
        [Display(Name ="Nazwa placówki")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić krótki opis placówki")]
        [Display(Name = "Króki opis placówki")]
        public string Description { get; set; }
        [Display(Name = "Zdjęcie (300x500 pikseli)")]
        public IFormFile ImageFile { get; set; }
        [Display(Name = "Zdjęcie (300x500 pikseli)")]
        public string ImagePath { get; set; }
        public string ImageSource
        {
            get
            {
                if (ImageFile != null)
                {
                    if (ImageFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            ImageFile.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);


                            return string.Format("data:image/jpg;base64,{0}", s);
                        }
                    }

                }
                return ImagePath;
            }
        }
        [Display(Name = "Numer telefonu")]
        [Required(ErrorMessage = "Proszę wprowadzić numer telefonu do placówki")]
        [DataType(DataType.PhoneNumber)]

        public string PhoneNumber { get; set; }
        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Proszę wprowadzić kod pocztowy")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        //public VoivodeshipType VoivodeshipType { get; set;}
        [Display(Name = "Aglomeracja")]
        [Required(ErrorMessage ="Proszę o wybranie aglomeracji")]
        public Aglomeration? Aglomeration { get; set; }
        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Proszę wprowadzić nazwę miasta")]
        [DataType(DataType.Text)]
        public string City { get; set; }
        [Display(Name = "Nazwa ulicy oraz numer budynku")]
        [Required(ErrorMessage = "Proszę wprowadzić nazwę ulicy oraz numer budynku")]
        [DataType(DataType.Text)]
        public string StreetAndNumber { get; set; }
        public IEnumerable<string> Facilities { get; set; }
        [Display(Name = "Świadczone usługi")]
        [Required(ErrorMessage = "Proszę wybrać świadczone usługi")]
        public List<long> MedicalServiceIds { get; set; }
        public List<MedicalService> Services { get; set; }
        [Display(Name = "Lista pokoi")]
        [Required(ErrorMessage = "Wybierz pokoje")]
        public List<long> MedicalRoomIds { get; set; }
        private List<MedicalRoom> _medicalRooms;
        public List<MedicalRoom> MedicalRooms 
        {
            get
            {
                return _medicalRooms;
            }
            set
            {
                _medicalRooms = value;
                if (value!=null)
                {
                    SetRoomsBackReferences();
                }
            }
        }
        public void SetRoomsBackReferences()
        {
            foreach (MedicalRoom item in MedicalRooms)
            {
                item.Location = this;
            }
        }
        public bool IsValid 
        { 
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    if (!string.IsNullOrWhiteSpace(Description))
                    {
                        if (Aglomeration.HasValue)
                        {
                            if (!string.IsNullOrWhiteSpace(City))
                            {
                                if (!string.IsNullOrWhiteSpace(StreetAndNumber))
                                {
                                    if (Services?.Count>0)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }
            
        }

    }
}
