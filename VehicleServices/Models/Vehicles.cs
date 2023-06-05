using Microsoft.AspNetCore.Mvc.Rendering;

namespace VehicleServices.Models
{
    public class Vehicles
    {
        public IEnumerable<SelectListItem>? vehicleMakeList { get; set; }
        public VehicleModel vehicleModel { get; set; }
    }
}