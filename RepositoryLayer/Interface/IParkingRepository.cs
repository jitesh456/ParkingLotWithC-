using System;
using System.Collections.Generic;
using ParkingLotModelLayer;

namespace RepositoryLayer
{
    public interface IParkingRepository
    {
        IEnumerable<ParkingLot> GetAllParkingData();
        bool Park(ParkingLot parkingLot);
        IEnumerable<ParkingLot> searchParkingData(string vehicleNumber);
        bool Unpark(string vehicleNumber);
        IEnumerable<ParkingLot> searchParkingDataBySlotNumber(int slotNumber);
    }
}