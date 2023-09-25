using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class LocationsManageViewModel: IBaseViewModel
    {
        public long SelectedLocationId { get; set; }
        public Location SelectedLocation { get; set; }
        public List<Location> AllLocations { get; set; }
        public ViewMode ViewMode { get; set; }
        public bool IsValid { get; internal set; }
        [Display(Name = "Lista świadczonych w placówce usług")]//, odznacz i zatwierdź, by usunąć")]
        public List<long> MedicalServiceIds { get; set; }
        [Display(Name = "Lista nieprzypisanych do żadnej placówki pokoi. Zaznacz i zatwierdź, by dodać")]
        public List<long> MedicalRoomIdsToAdd { get; set; }
        [Display(Name = "Lista pokoi w placówce, odznacz i zatwierdź, by usunąć")]
        public List<long> MedicalRoomIdsCurrent { get; set; }
        public List<MedicalService> PrimaryServices { get; set; }
        public List<MedicalRoom> UnasignedRooms { get; set; }
        public string UserName { get; set; }
        public ViewMessage ViewMessage { get; set; } = new ViewMessage();

        internal void UpdateSelectionOfRooms()
        {
            List<MedicalRoom> newRoomsList = new List<MedicalRoom>();
            if (SelectedLocation.MedicalRooms==null)
            {
                
            }
            if (MedicalRoomIdsCurrent?.Count>0)
            {
                foreach (long itemNum in MedicalRoomIdsCurrent)
                {
                    MedicalRoom room = SelectedLocation.MedicalRooms.First(c => c.Id == itemNum);
                    newRoomsList.Add(room);
                }
            }
            if (UnasignedRooms?.Count>0)
            {
                if (MedicalRoomIdsToAdd?.Count>0)
                {
                    foreach (long itemNum in MedicalRoomIdsToAdd)
                    {
                        MedicalRoom room = UnasignedRooms.First(c => c.Id == itemNum);
                        newRoomsList.Add(room);
                    }
                }
            }
            SelectedLocation.MedicalRooms = newRoomsList;
        }

        internal void UpdateSelectionOfServices()
        {
            SelectedLocation.Services = new List<MedicalService>();
            foreach (long item in MedicalServiceIds)
            {
                MedicalService medicalService = PrimaryServices.First(c => c.Id == item);
                SelectedLocation.Services.Add(medicalService);
            }
        }

        internal void GetRooms(IAdministrationModuleRepository context, Location selectedLocation)
        {
            if (SelectedLocation.MedicalRoomIds==null)
            {
                return;
            }
            SelectedLocation.MedicalRooms = new List<MedicalRoom>();

            foreach (long id in SelectedLocation.MedicalRoomIds)
            {
                MedicalRoom medicalRoom = context.GetRoomById(id);
                SelectedLocation.MedicalRooms.Add(medicalRoom);
            }
        }

        internal void GetServices(IAdministrationModuleRepository context, Location selectedLocation)
        {
            SelectedLocation.Services = new List<MedicalService>();
            foreach (long id in SelectedLocation.MedicalServiceIds)
            {
                MedicalService service = context.GetMedicalServiceById(id);
                SelectedLocation.Services.Add(service);
            }
        }
    }
}
