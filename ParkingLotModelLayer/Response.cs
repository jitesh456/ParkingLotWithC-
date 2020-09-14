using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ParkingLotModelLayer
{
    public class Response
    {
        public HttpStatusCode StateCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

    }
}
