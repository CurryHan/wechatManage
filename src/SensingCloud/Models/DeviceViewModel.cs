using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using rs = Resources.Resources;

namespace SensingCloud.Models
{
    public class CreateDeviceViewModel
    {
        public CreateDeviceViewModel()
        {
            FloorItems = new List<SelectListItem>();
            RoomItems = new List<SelectListItem>();
        }

        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = nameof(rs.DeviceImage))]
        public string ImageUrl { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Resources), Name = "DeviceName")]
        [MaxLength(30)]
        public string Name { get; set; }

        [RegularExpression(@"^\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}$", ErrorMessageResourceName = nameof(rs.InvalidIP), ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(ResourceType = typeof(Resources.Resources), Name = "DeviceIP")]
        public string IP { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "DeviceMac")]
        [RegularExpression(@"^([0-9A-F]{2}[:-]){5}([0-9A-F]{2})$", ErrorMessageResourceName = nameof(rs.InvalidMac), ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Mac { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Resources), Name = "DeviceType")]
        public int SelectedDeviceTypeId { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> DeviceTypeItems { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Resources), Name = "DeviceGroup")]
        public int SelectedGroupId { get; set; }
        public IEnumerable<SelectListItem>  GroupItems { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Resources), Name =nameof(rs.Building))]
        public int SelectedBuildingId { get; set; }
        public IEnumerable<SelectListItem> BuildingItems { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Resources), Name = nameof(rs.Floor))]
        public int SelectedFloorId { get; set; }
        public IEnumerable<SelectListItem> FloorItems { get; set; }
       
        [Required]
        [Display(ResourceType = typeof(Resources.Resources), Name = nameof(rs.Room))]
        public int SelectedRoomId { get; set; }
        public IEnumerable<SelectListItem> RoomItems { get; set; }

    }

    public class DeviceDetailViewModel
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = nameof(rs.DeviceImage))]
        public string ImageUrl { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Resources),Name = "DeviceName")]
        [MaxLength(30)]
        public string Name { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "Longtitude")]
        public float Longitude { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "Latitude")]
        public float Latitude { get; set; }
        [MaxLength(20)]
        [Display(ResourceType = typeof(Resources.Resources), Name = "Building")]
        public string Building { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "Floor")]
        [MaxLength(20)]
        public string Floor { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "Room")]
        public string Room { get; set; }
        [MaxLength(20)]
        [Display(ResourceType = typeof(Resources.Resources), Name = nameof(rs.Target))]
        public string Target { get; set; }
        [RegularExpression(@"^\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}$", ErrorMessageResourceName = nameof(rs.DeviceIP), ErrorMessageResourceType =typeof(Resources.Resources))]
        [Display(ResourceType = typeof(Resources.Resources), Name = "DeviceIP")]
        public string IP { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "DeviceMac")]
        [RegularExpression(@"^([0-9A-F]{2}[:-]){5}([0-9A-F]{2})$",ErrorMessageResourceName = nameof(rs.DeviceMac),ErrorMessageResourceType = typeof(Resources.Resources))]
        public string Mac { get; set; }
        [MaxLength(50)]
        [Display(ResourceType = typeof(Resources.Resources), Name = "Location")]
        public string Location { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "LocationX")]
        public float LocationX { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "LocationY")]
        public float LocationY { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "DeviceType")]
        public string  DeviceTypeName{ get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "DeviceGroup")]
        public string DeviceGroupName { get; set; }

    }

    public class EditDeviceTypeViewModel
    {
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}