using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotBusnessLayer
{
    public interface IMessagingService
    {
        void Send(string message);
        void Receive();
    }
}
