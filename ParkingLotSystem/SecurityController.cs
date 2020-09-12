using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotModelLayer;
using ParkingLotBusnessLayer;


namespace ParkingLotSystem
{
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private readonly IParking parking;

        public SecurityController(IParking parking)
        {

            this.parking = parking;
        }


        // GET: api/<controller>
        [HttpGet("parking")]
        public ActionResult<IEnumerable<ParkingLot>> GetAllParking()
        {

            try
            {
                IEnumerable<ParkingLot> parkingList = parking.GetAllParkingData();
                if (parkingList.Count() > 0)
                {
                    return this.Ok(new Response() { Success = true, Message = "Parking List", Data = parkingList, });
                }
                return this.Ok(new Response() { Success = false, Message = "No Record Found", Data = null });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { Success = false, Message = e.Message, Data = null, });

            }

        }



        [HttpPost("park")]
        public ActionResult<Boolean> ParkVehicle([FromBody] ParkingLot parkingLot)
        {
            try
            {
                Boolean result = parking.Park(parkingLot);
                if (result)
                {
                    return this.Ok(new Response() { Success = true, Message = "Vehicle Parked", Data = null, });
                }
                return this.Ok(new Response() { Success = false, Message = "Vehicle failed to Parked", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { Success = false, Message = e.Message, Data = null, });

            }
        }


        [HttpPut("unpark")]
        public ActionResult<Boolean> UnPark(string vehicleNumber)
        {
            try
            {
                Boolean result = parking.Unpark(vehicleNumber);
                if (result)
                {
                    return this.Ok(new Response() { Success = true, Message = "Vehicle UnParked", Data = null, });
                }
                return this.Ok(new Response() { Success = false, Message = "Vehicle Not found", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { Success = false, Message = e.Message, Data = null, });

            }

        }


        [HttpGet("search{slotNumber:int}")]
        public ActionResult<IEnumerable<ParkingLot>> SeachVehicleBySlotNumber(int slotNumber)
        {
            try
            {
                IEnumerable<ParkingLot> parkingData = parking.SearchVehicleSlotNumber(slotNumber);
                if (parkingData.Count() > 0)
                {
                    return this.Ok(new Response() { Success = true, Message = "Vehicle Info", Data = parkingData, });
                }
                return this.Ok(new Response() { Success = false, Message = "Vehicle Not found", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { Success = false, Message = e.Message, Data = null, });

            }

        }

        [HttpGet("search{VehicleNumber}")]
        public ActionResult<IEnumerable<ParkingLot>> SeachVehicleByVehicleNumber(string vehicleNumber)
        {
            try
            {
                IEnumerable<ParkingLot> parkingData = parking.SearchVehicle(vehicleNumber);
                if (parkingData.Count() > 0)
                {
                    return this.Ok(new Response() { Success = true, Message = "Vehicle Info", Data = parkingData, });
                }
                return this.Ok(new Response() { Success = false, Message = "Vehicle Not found", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { Success = false, Message = e.Message, Data = null, });

            }

        }
    }
}
