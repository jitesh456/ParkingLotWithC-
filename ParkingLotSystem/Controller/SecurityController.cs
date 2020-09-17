using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotModelLayer;
using ParkingLotBusnessLayer;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace ParkingLotSystem
{
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IParking parking;
        private readonly IMessagingService messagingService;

        public SecurityController(IParking parking,IMessagingService messagingService)
        {
            this.parking = parking;
            this.messagingService = messagingService;         
        }


        [Authorize(Roles = "Security")]
        [HttpGet("parking")]
        public ActionResult<IEnumerable<ParkingLot>> GetAllParking()
        {
            try
            {
                IEnumerable<ParkingLot> parkingList = parking.GetAllParkingData();
                if (parkingList.Count() > 0)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Parking List", Data = parkingList, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "No Record Found", Data = parkingList });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }

        [Authorize(Roles = "Security")]
        [HttpPost("park")]
        public ActionResult<Boolean> ParkVehicle([FromBody] ParkingLot parkingLot)
        {
            try
            {
                Boolean result = parking.Park(parkingLot);
                if (result)
                {
                    this.messagingService.Send("Security parked at slot no:"+parkingLot.SlotNumber );
                    this.messagingService.Receive();
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle Parked", Data = null, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Vehicle failed to Parked", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }

        [Authorize(Roles = "Security")]
        [HttpPut("unpark")]
        public ActionResult<Boolean> UnPark(string vehicleNumber)
        {
            try
            {
                Boolean result = parking.Unpark(vehicleNumber);
                if (result)
                {
                    this.messagingService.Send("Security with vehicle no:" + vehicleNumber + " Unparked " );
                    this.messagingService.Receive();
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle UnParked", Data = null, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Vehicle Not found", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }

        [Authorize(Roles = "Security")]
        [HttpGet("search/{slotNumber:int}")]
        public ActionResult<IEnumerable<ParkingLot>> SeachVehicleBySlotNumber(int slotNumber)
        {
            try
            {
                IEnumerable<ParkingLot> parkingData = parking.SearchVehicleSlotNumber(slotNumber);
                if (parkingData.Count() > 0)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle found at Slot number:"+slotNumber, Data = parkingData, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Vehicle Not found", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }

        [Authorize(Roles = "Security")]
        [HttpGet("search/{VehicleNumber}")]
        public ActionResult<IEnumerable<ParkingLot>> SeachVehicleByVehicleNumber(string vehicleNumber)
        {
            try
            {
                IEnumerable<ParkingLot> parkingData = parking.SearchVehicle(vehicleNumber);
                if (parkingData.Count() > 0)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle no "+vehicleNumber+" found", Data = parkingData, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Vehicle Not found", Data = parkingData, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }
    }
}
