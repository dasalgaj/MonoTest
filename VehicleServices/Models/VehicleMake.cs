using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleServices.Models
{
    public class VehicleMake
    {
        [Key]
        public int id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Abrv")]
        public string abrv { get; set; }
    }
}