using System.Collections.Generic;
using ParkingLotModelLayer;

namespace ParkingLotBusnessLayer
{
    public interface IParking
    {
        IEnumerable<ParkingLot> GetAllParkingData();
        bool Park(ParkingLot parkingLot);
        IEnumerable<ParkingLot> SearchVehicle(string searchField);
        bool Unpark(string vehicleNumber);
        IEnumerable<ParkingLot> SearchVehicleSlotNumber(int slotNumber);
    }
}