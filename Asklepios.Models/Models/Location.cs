﻿using Asklepios.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace Asklepios.Core.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        [Required(ErrorMessage ="Proszę wprowadzić nazwę placówki")]
        [Display(Name ="Nazwa placówki")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić krótki opis placówki")]
        [Display(Name = "Króki opis placówki")]
        public string Description { get; set; }
        [Display(Name = "Zdjęcie (300x500 pikseli)")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Display(Name = "Zdjęcie (300x500 pikseli)")]
        public string ImagePath { get; set; }
        //source for web image
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
        //public IEnumerable<Facility> Facilities { get; set; }
        [Display(Name = "Świadczone usługi")]
        [Required(ErrorMessage = "Proszę wybrać świadczone usługi")]
        [NotMapped]
        public List<long> MedicalServiceIds { get; set; }
        public List<MedicalService> Services { get; set; }

        //[Required(ErrorMessage = "Proszę szerokość geograficzną placówki!")]
        //[Display(Name = "Szerokość geograficzna")]
        //public string Latitude { get; set; }
        //[Required(ErrorMessage = "Proszę długość geograficzną placówki!")]
        //[Display( Name ="Długość geograficzna")]
        //public string Longtitude { get; set; }





        [Display(Name = "Lista pokoi")]
        [Required(ErrorMessage = "Wybierz pokoje")]
        [NotMapped]
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
            }
        }
        public void SetRoomsBackReferences()
        {
            foreach (MedicalRoom item in MedicalRooms)
            {
                item.Location = this;
                item.LocationId = this.Id;
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
                                    if (Services?.Count>0 || MedicalServiceIds?.Count>0)
                                    {
                                        if (ImageFile!=null || string.IsNullOrWhiteSpace( ImagePath)==false)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }           
        }

        public void UpdateWithAnotherLocation(Location selectedLocation)
        {
            if (this.PostalCode!=selectedLocation.PostalCode)
            {
                this.PostalCode = selectedLocation.PostalCode;
            }
            if (this.Aglomeration!=selectedLocation.Aglomeration)
            {
                this.Aglomeration = selectedLocation.Aglomeration;
            }
            if (this.City!=selectedLocation.City)
            {
                this.City = selectedLocation.City;
            }
            if (this.PhoneNumber!=selectedLocation.PhoneNumber)
            {
                this.PhoneNumber = selectedLocation.PhoneNumber;
            }
            if (this.Description!=selectedLocation.Description)
            {
                this.Description = selectedLocation.Description;
            }
            if (this.StreetAndNumber!=selectedLocation.StreetAndNumber)
            {
                this.StreetAndNumber = selectedLocation.StreetAndNumber;
            }
            if (this.ImagePath!=selectedLocation.ImagePath)
            {
                this.ImagePath = selectedLocation.ImagePath;
            }
            if (this.Name!=selectedLocation.Name)
            {
                this.Name = selectedLocation.Name;
            }

            foreach (var item in selectedLocation.Services)
            {
                if (this.Services.Any(c=>c.Id==item.Id))
                {

                }
                else
                {
                    this.Services.Add(item);
                }
            }
            for (int i = this.Services.Count-1; i >= 0; i--)
            {
                MedicalService service = this.Services[i];

                if (selectedLocation.Services.Any(c => c.Id == service.Id))
                {

                }
                else
                {
                    this.Services.Remove(service);
                }
            }

            //foreach (var item in selectedLocation.Services)
            //{
            //    if (this.Services.Any(c => c.Id == item.Id))
            //    {

            //    }
            //    else
            //    {
            //        this.Services.Add(item);
            //    }
            //}
            //for (int i = this.Services.Count - 1; i >= 0; i--)
            //{
            //    MedicalService service = this.Services[i];

            //    if (selectedLocation.Services.Any(c => c.Id == service.Id))
            //    {

            //    }
            //    else
            //    {
            //        this.Services.Remove(service);
            //    }
            //}

        }
    }
}
