using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkingLotBusnessLayer.Interface;
using ParkingLotModelLayer;

namespace ParkingLotSystem.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("/Login")]
        public IActionResult Login([FromBody]User user)
        {
            try
            {
                string Token = userService.Login(user);
                if (Token.Length>3)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "Token", Data = Token, });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Login Failed", Data =Token });
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });

            }

        }

        [HttpPost]
        public IActionResult AddUser([FromBody]UserDetails userDetails)
        {
            try
            {
                bool result=userService.AddUser(userDetails);
                if (result)
                {
                    return this.Ok(new Response() { StateCode = HttpStatusCode.OK, Message = "User Added successfully" });
                }
                return this.NotFound(new Response() { StateCode = HttpStatusCode.NotFound, Message = "Failed  To Add"});
            }
            catch (Exception e)
            {
                return this.BadRequest(new Response() { StateCode = HttpStatusCode.BadRequest, Message = e.Message, Data = null, });

            }
        }
    }
 
}
