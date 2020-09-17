using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingLotModelLayer;

namespace RepositoryLayer.Interface
{
   public interface IUserRepository
    {
        Boolean AddUser(UserDetails userDetails);
        string Login(User user);
    }
}
