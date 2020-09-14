﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotModelLayer;
using ParkingLotBusnessLayer;
using System.Net;

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
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Parking List", Data = parkingList, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "No Record Found", Data = parkingList });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });

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
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle Parked", Data = null, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Vehicle failed to Parked", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
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
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle UnParked", Data = null, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Vehicle Not found", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }


        [HttpGet("search/{slotNumber:int}")]
        public ActionResult<IEnumerable<ParkingLot>> SeachVehicleBySlotNumber(int slotNumber)
        {
            try
            {
                IEnumerable<ParkingLot> parkingData = parking.SearchVehicleSlotNumber(slotNumber);
                if (parkingData.Count() > 0)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle Info", Data = parkingData, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Vehicle Not found", Data = null, });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });
            }
        }

        [HttpGet("search/{VehicleNumber}")]
        public ActionResult<IEnumerable<ParkingLot>> SeachVehicleByVehicleNumber(string vehicleNumber)
        {
            try
            {
                IEnumerable<ParkingLot> parkingData = parking.SearchVehicle(vehicleNumber);
                if (parkingData.Count() > 0)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Vehicle Info", Data = parkingData, });
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
