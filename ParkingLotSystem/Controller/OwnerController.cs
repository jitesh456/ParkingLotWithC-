using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotBusnessLayer;
using ParkingLotModelLayer;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ParkingLotSystem
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner")]
    public class OwnerController : ControllerBase
    {
        private readonly IParking parking;
        private readonly IMessagingService messagingService;
        private readonly IOwnerService ownerService;

        public OwnerController(IParking parking,IOwnerService ownerService,IMessagingService messagingService)
        {
            this.messagingService = messagingService;
            this.parking = parking;
            this.ownerService = ownerService;
        }

        ///<response code = "401" >UnAuthorized</response>
        ///<response code = "403" >Access Denied</response>
        [HttpGet("parking")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        ///<response code = "401" >UnAuthorized</response>
        ///<response code = "403" >Access Denied</response>
        [HttpPost("park")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Boolean> ParkVehicle([FromBody] ParkingLot parkingLot)
        {
            try
            {
                this.messagingService.Send("Owner parked at slot no:" + parkingLot.SlotNumber);
                this.messagingService.Receive();

                Boolean result = parking.Park(parkingLot);
                if (result)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle Parked", Data = null, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Vehicle failed to Parked", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }

        ///<response code = "401" >UnAuthorized</response>
        ///<response code = "403" >Access Denied</response>
        [HttpPut("unpark")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Boolean> UnPark(string vehicleNumber)
        {
            try
            {
                Boolean result = parking.Unpark(vehicleNumber);
                if (result)
                {
                    this.messagingService.Send("Owner with vehicle no:" + vehicleNumber + " Unparked ");
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

        ///<response code = "401" >UnAuthorized</response>
        ///<response code = "403" >Access Denied</response>
        [HttpGet("search/{slotNumber:int}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<IEnumerable<ParkingLot>> SeachVehicleBySlotNumber(int slotNumber)
        {
            try
            {
                IEnumerable<ParkingLot> parkingData = parking.SearchVehicleSlotNumber(slotNumber);
                if (parkingData.Count() > 0)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle found at Slot number:" + slotNumber, Data = parkingData, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Vehicle Not found", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }

        ///<response code = "401" >UnAuthorized</response>
        ///<response code = "403" >Access Denied</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<IEnumerable<ParkingLot>> SeachVehicleByVehicleNumber(string vehicleNumber)
        {
            try
            {
                IEnumerable<ParkingLot> parkingData = parking.SearchVehicle(vehicleNumber);
                if (parkingData.Count() > 0)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle no " + vehicleNumber + " found", Data = parkingData, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Vehicle Not found", Data = parkingData, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }

        ///<response code = "401" >UnAuthorized</response>
        ///<response code = "403" >Access Denied</response>
        [HttpGet("emptyParkingSlot")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<SlotInformation>> GetEmptySlotNumber()
        {
            try
            {
                IEnumerable<SlotInformation> emptySlotList = ownerService.GetEmptySlotNumber();
                if (emptySlotList.Count() > 0)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Empty Parking Slot List", Data = emptySlotList, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = " Not Record Found", Data = emptySlotList, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }
    }
}
