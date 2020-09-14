using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotModelLayer
{
    public class ParkingLot
    {
       
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string VehicleNumber { get; set; }

        [Required]
        public int DriverType { get; set; }

        [Required]
        public int VehicleType { get; set; }

        [Required]
        public int ParkingType { get; set; }

        [Required]
        public int SlotNumber { get; set; }
    }
}
