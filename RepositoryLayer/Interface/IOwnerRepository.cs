using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface IOwnerRepository
    {
        IEnumerable<SlotInformation> GetEmptySlot();
    }
}
