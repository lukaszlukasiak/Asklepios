using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class ScheduleItemsAddViewModel : IBaseViewModel
    {
        //public List<Visit> Schedule { get; set; }
        public List<MedicalRoom> MedicalRooms { get; set; }
        public List<MedicalWorker> MedicalWorkers { get; set; }
        public List<MedicalService> PrimaryMedicalServices { get; set; }
        public List<Location> Locations { get; set; }
        public List<VisitCategory> VisitCategories { get; set; }
        public MedicalWorker SelectedMedicalWorker { get; set; }
        [Required(ErrorMessage = "Proszę wybierz placówkę")]
        [Display(Name = "Placówka medyczna")]
        public string SelectedLocationId { get; set; }
        [Required(ErrorMessage = "Proszę wybierz pokój")]
        [Display(Name = "Pokój medyczny")]

        public string SelectedRoomId { get; set; }
        [Required(ErrorMessage = "Proszę wybierz pracownika medycznego")]
        [Display(Name = "Pracownik medyczny")]
        public string SelectedMedicalWorkerId { get; set; }
        [Required(ErrorMessage = "Proszę wybierz usługę medyczną")]
        [Display(Name = "Usługa medyczna")]

        public string SelectedMedicalServiceId { get; set; }
        [Display(Name = "Kategoria wizyty")]
        [Required(ErrorMessage = "Proszę wybierz kategorię wizyty medycznego")]

        public string SelectedVisitCategoryId { get; set; }

        [Required(ErrorMessage = "Proszę wybierz liczbę wizyt do dodania")]
        [Range(1, 48)]
        [Display(Name = "Liczba kolejnych wizyt do dodania")]
        public int NumberOfVisitsToAdd { get; set; } = 10;
        [Required(ErrorMessage = "Proszę wybierz długość pojedynczej wizyty")]
        [Range(10, 60)]
        [DataType(DataType.Duration)]
        [Display(Name = "Czas trwania wizyty")]
        public int DurationOfVisit { get; set; } = 10;

        //public TimeSpan? _firstVisitTime;
        [Required(ErrorMessage = "Proszę wybierz godzinę pierwszej wizyty")]
        //[DisplayFormat(ApplyFormatInEditMode = true)]
        [Display(Name = "Godzina pierwszej wizyty")]
        [DataType(DataType.Time)]
        public TimeSpan FirstVisitTime { get; set; } = new TimeSpan(8, 0, 0);

        private const string ERROR_MESSAGE_ROOM = "Wybrany pokój jest zajęty w danym zakresie czasu. Wybierz inny.";
        private const string ERROR_MESSAGE_WORKER = "Wybrany pracownik jest zajęty w danym zakresie czasu. Wybierz innego, albo zmień czas wizyt.";
        private const string ERROR_MESSAGE_DATE = "Dodawane wizyty muszą zostać dodane przynajmniej z jednodniowym wyprzedzeniem!";
        private const string ERROR_MESSAGE_NUMBER_OF_VISITS = "Możesz dodać jednorazowo do 1 do 48 kolejnych wizyt!";
        private const string ERROR_MESSAGE_DURATION = "Wizyty mogą trwać od 10 do 60 minut!";
        public string ErrorMessage { get; set; }
        public string Guard { get; set; }


        private DateTime? _firstVisitInitialDateTime;
        [Required(ErrorMessage = "Proszę wybierz datę")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Display(Name = "Data dodawanych wizyt")]
        public DateTime VisitsDate
        {
            get
            {
                if (_firstVisitInitialDateTime.HasValue)
                {
                    return _firstVisitInitialDateTime.Value;
                }
                else
                {
                    return DateTime.Now.Date.AddDays(1);
                }
            }
            set
            {
                _firstVisitInitialDateTime = value;
            }
        }

        public string SuccessMessage { get; internal set; }
        public string UserName { get; set; }

        public bool IsValid()
        {
            if (long.TryParse(SelectedVisitCategoryId, out long s1))
            {
                if (long.TryParse(SelectedRoomId, out long s2))
                {
                    if (long.TryParse(SelectedMedicalServiceId, out long s3))
                    {
                        if (long.TryParse(SelectedMedicalWorkerId, out long s4))
                        {
                            if (long.TryParse(SelectedLocationId, out long s5))
                            {
                                if (VisitsDate.Date >= DateTimeOffset.Now.Date.AddDays(1))
                                {
                                    if (NumberOfVisitsToAdd > 1 && NumberOfVisitsToAdd <= 48)
                                    {
                                        if (DurationOfVisit >= 10 && DurationOfVisit <= 60)
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            ErrorMessage = ERROR_MESSAGE_DURATION;
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage = ERROR_MESSAGE_NUMBER_OF_VISITS;
                                    }
                                }
                                else
                                {
                                    ErrorMessage = ERROR_MESSAGE_DATE;
                                }
                            }
                            else
                            {
                            }
                        }
                    }
                }
            }
            return false;
        }


        public bool IsDuplicated(IQueryable<Visit> visits)
        {

            DateTime startDate = VisitsDate.Add(FirstVisitTime);
            DateTime endDate = startDate.Add(new TimeSpan(0, NumberOfVisitsToAdd * DurationOfVisit, 0));
            if (long.TryParse(SelectedRoomId, out long roomdId))
            {
                if (long.TryParse(SelectedMedicalWorkerId, out long workerId))
                {
                    if (roomdId > 0 && workerId > 0)
                    {
                        IQueryable<Visit> filteredVisits = visits.Where(c => c.DateTimeSince.Date == VisitsDate.Date  && c.MedicalRoom.Id == roomdId).AsQueryable(); //.ToList();
                   //     List<Visit> fvisits= visits.Where(c => c.DateTimeSince.Date == VisitsDate.Date && c.MedicalRoom.Id == roomdId).ToList(); //.ToList();

                        TimeSpan start = FirstVisitTime;
                        TimeSpan end = VisitsDate.TimeOfDay.Add(new TimeSpan(0, NumberOfVisitsToAdd * DurationOfVisit, 0)).Add(FirstVisitTime);


                        IQueryable<Visit> duplicates = filteredVisits.Where(c => (c.DateTimeSince.TimeOfDay >= start && c.DateTimeSince.TimeOfDay < end) || (c.DateTimeTill.TimeOfDay > start && c.DateTimeTill.TimeOfDay <= end)).AsQueryable();// .ToList();
                      //  fvisits= filteredVisits.Where(c => (c.DateTimeSince.TimeOfDay > start && c.DateTimeSince.TimeOfDay < end) || (c.DateTimeTill.TimeOfDay > start && c.DateTimeTill.TimeOfDay < end)).ToList();
                        if (duplicates != null && duplicates.Count() > 0)
                        {
                            ErrorMessage = ERROR_MESSAGE_ROOM;
                            return true;
                        }

                        filteredVisits = visits.Where(c => c.DateTimeSince.Date == VisitsDate.Date && c.MedicalWorker.Id == workerId).AsQueryable();//.ToList();
                      //  fvisits = visits.Where(c => c.DateTimeSince.Date == VisitsDate.Date && c.MedicalWorker.Id == workerId).ToList();
                        duplicates = filteredVisits.Where(c => (c.DateTimeSince.TimeOfDay >= start && c.DateTimeSince.TimeOfDay < end) || (c.DateTimeTill.TimeOfDay > start && c.DateTimeTill.TimeOfDay <= end)).AsQueryable(); //.ToList();
                        if (duplicates != null && duplicates.Count() > 0)
                        {
                            ErrorMessage = ERROR_MESSAGE_WORKER;
                            return true;
                        }

                    }

                }
            }
            return false;
        }
    }
}
