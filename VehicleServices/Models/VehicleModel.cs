using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleServices.Models
{
    public class VehicleModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Abrv")]
        public string abrv { get; set; }

        //Foreign key
        public int make_id { get; set; }
        [ForeignKey("make_id")]
        public virtual VehicleMake? VehicleMake { get; set; }
    }
}