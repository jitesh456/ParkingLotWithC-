using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotBusnessLayer.Interface
{
    public interface IUserService
    {
        string Login(User user);
        Boolean AddUser(UserDetails userDetails);
    }
}
