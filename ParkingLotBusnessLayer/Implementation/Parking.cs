namespace ParkingLotBusnessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RepositoryLayer;
    using ParkingLotModelLayer;
    
    public class Parking : IParking
    {
        
        private readonly IParkingRepository parkingLotRepository;

        public Parking(IParkingRepository parkingLotRepository)
        {
            this.parkingLotRepository = parkingLotRepository;
        }

        public Boolean Park(ParkingLot parkingLot)
        {
            try
            {
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
                return parkingLotRepository.Unpark(vehicleNumber);
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

        public IEnumerable<ParkingLot> SearchVehicleSlotNumber(int slotNumber)
        {
            try
            {
                return parkingLotRepository.searchParkingDataBySlotNumber(slotNumber);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
