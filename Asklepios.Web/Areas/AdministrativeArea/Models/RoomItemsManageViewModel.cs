using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class RoomItemsManageViewModel
    {
        public long SelectedRoomId { get; set; }
        public MedicalRoom SelectedRoom { get; set; }
        //public MedicalRoom SelectedRoom { get; set; }
        public MedicalRoom NewRoom { get; set; }
        public List<Location> Locations { get; set; }
        public List<Location> SortedLocations
        {
            get
            {
                if (Locations!=null)
                {
                    return Locations.OrderBy(c => c.Name).ToList();
                }
                else
                {
                    return null;
                }
            }
        }
        public Location SelectedLocation { get; set; }
        [Display(Name = "Filtruj po placówce medycznej")]

        public long SelectedLocationId { get; set; }


        public List<MedicalRoom> FilteredRooms
        {
            get
            {
                if (SelectedLocation!=null)
                {
                    List<MedicalRoom> sortRooms = SortedRooms;
                    List<MedicalRoom> rooms = sortRooms.Where(c => c.Location?.Id == SelectedLocation.Id).ToList();
                    return rooms;
                    //return SortedRooms.Where(c => c.Location == SelectedLocation).ToList();
                }
                else
                {
                    return SortedRooms;
                }
                
            }
        }

        public List<MedicalRoom> SortedRooms
        {
            get
            {
                if (AllRooms?.Count>0)
                {
                    //return AllRooms.Sort(c => c.Location != null ? c.Location.Name : "");
                    return AllRooms.OrderBy(c => (c.Location != null ? c.Location.Name : Char.ConvertFromUtf32(1))).ToList();

                }
                else
                {
                    return null;
                }
            }
        }
        public List<MedicalRoom> AllRooms { get; set; }
        public ViewMode ViewMode { get; set; }
        public bool IsValid { get; internal set; }
        public List<MedicalRoom> UnasignedRooms { get; set; }


    }
}
