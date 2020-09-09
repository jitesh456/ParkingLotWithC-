using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotModelLayer
{
    public class ParkingLot
    {
        public int Id { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public int DriverType { get; set; }
        public Boolean Disable { get; set; }
        public int VehicleType { get; set; }
        public int ParkingType { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int SlotNumber { get; set; }
    }
}
