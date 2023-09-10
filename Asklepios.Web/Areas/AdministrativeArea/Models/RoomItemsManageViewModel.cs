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
    public class RoomItemsManageViewModel : IBaseViewModel
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
                List<MedicalRoom> sortRooms = SortedRooms;
                if (sortRooms==null)
                {
                    return null;
                }
                if (SelectedLocation!=null)
                {
                    List<MedicalRoom> rooms = sortRooms.Where(c => c.Location?.Id == SelectedLocation.Id).ToList();
                    return rooms;
                    //return SortedRooms.Where(c => c.Location == SelectedLocation).ToList();
                }
                else if (SelectedLocationId==-2)
                {
                    var rooms = sortRooms.Where(c => c.LocationId == 0);
                    if (rooms == null)
                    {
                        return null;
                    }
                    else
                    {
                        return rooms.ToList();
                    }

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
        //public List<MedicalRoom> UnasignedRooms { get; set; }
        public string UserName { get; set; }
        //public string Message { get; set; }
        //public AlertMessageType AlertMessageType { get; set; }
        public ViewMessage ViewMessage { get; set; } = new ViewMessage();
    }
}
