using System;
using System.Collections.Generic;
using ParkingLotModelLayer;

namespace RepositoryLayer
{
    public interface IParkingLotRepository
    {
        IEnumerable<ParkingLot> GetAllParkingData();
        bool Park(ParkingLot parkingLot);
        IEnumerable<ParkingLot> searchParkingData(string SearchField);
        bool Unpark(string vehicleNumber, DateTime modifiedTime);
    }
}