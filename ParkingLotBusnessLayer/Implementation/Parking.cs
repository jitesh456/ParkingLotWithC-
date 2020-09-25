namespace ParkingLotBusnessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RepositoryLayer;
    using ParkingLotModelLayer;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    public class Parking : IParking
    {
        private readonly IParkingRepository parkingLotRepository;
        private readonly IDistributedCache distributedCache;

        public Parking(IParkingRepository parkingLotRepository,IDistributedCache distributedCache)
        {
            this.parkingLotRepository = parkingLotRepository;
            this.distributedCache = distributedCache;
        }

        public Boolean Park(ParkingLot parkingLot)
        {
            try
            {
                if (distributedCache.GetString("ParkingData") != null)
                {
                    distributedCache.Remove("ParkingData");
                }
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
                if (distributedCache.GetString("ParkingData") != null)
                {
                    distributedCache.Remove("ParkingData");
                }
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
                IEnumerable<ParkingLot> parkingList = null;
                if (distributedCache.GetString("ParkingData") == null)
                {
                    parkingList = parkingLotRepository.GetAllParkingData();
                    distributedCache.SetString("ParkingData", JsonConvert.SerializeObject(parkingList));
                    return parkingList;
                }
                parkingList = JsonConvert.DeserializeObject<IEnumerable<ParkingLot>>(distributedCache.GetString("ParkingData"));
                return parkingList ;

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
                List<ParkingLot> parkingList = new List<ParkingLot>();
                if (distributedCache.GetString("ParkingData") != null)
                {
                    var parkingDataList = JsonConvert.DeserializeObject<IEnumerable<ParkingLot>>(distributedCache.GetString("ParkingData"));
                    foreach (ParkingLot parkingLot in parkingDataList)
                    {
                        if (parkingLot.VehicleNumber.Trim().Equals(searchField))
                        {
                            parkingList.Add(parkingLot);
                            return parkingList;
                        }
                    }
                }                
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
                List<ParkingLot> parkingList = new List<ParkingLot>();
                if (distributedCache.GetString("ParkingData") != null)
                {
                    var parkingDataList = JsonConvert.DeserializeObject<IEnumerable<ParkingLot>>(distributedCache.GetString("ParkingData"));
                    foreach (ParkingLot parkingLot in parkingDataList)
                    {
                        if (parkingLot.SlotNumber.Equals(slotNumber))
                        {
                            parkingList.Add(parkingLot);
                            return parkingList;
                        }
                    }
                }
                return parkingLotRepository.searchParkingDataBySlotNumber(slotNumber);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
