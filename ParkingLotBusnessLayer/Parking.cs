namespace ParkingLotBusnessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RepositoryLayer;
    using ParkingLotModelLayer.Dto;
    using ParkingLotModelLayer;


    public class Parking
    {
        private readonly DateTime currentTime;
        private readonly IParkingLotRepository parkingLotRepository;

        public Parking(IParkingLotRepository parkingLotRepository)
        {
            this.parkingLotRepository = parkingLotRepository;
            currentTime = default(DateTime);
        }

        public Boolean Park(ParkingLotDto parkingLotDto)
        {
            try
            {
                ParkingLot parkingLot = new ParkingLot();
                parkingLot.VehicleNumber = parkingLotDto.VehicleNumber;
                parkingLot.VehicleType = parkingLotDto.VehicleType;
                parkingLot.DriverType = parkingLotDto.DriverType;
                parkingLot.ParkingType = parkingLotDto.ParkingType;
                parkingLot.Disable = false;
                parkingLot.EntryTime = currentTime;
                parkingLot.ExitTime = default(DateTime);
                parkingLot.SlotNumber = 10;
                parkingLot.ModifiedTime = currentTime;
                return parkingLotRepository.Park(parkingLot);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Unpark(string vehicleNumber)
        {
            try
            {
                return parkingLotRepository.Unpark(vehicleNumber,currentTime);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ParkingLot> GetAllParkingData()
        {
            try
            {
                return parkingLotRepository.GetAllParkingData();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ParkingLot> SearchVehicle(string searchField)
        {
            try
            {
                return parkingLotRepository.searchParkingData(searchField);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
