using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer;
using ParkingLotModelLayer;

namespace ParkingLotBusnessLayer
{
    public class OwnerService:IOwnerService
    {
        public IOwnerRepository ownerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }

        public IEnumerable<SlotInformation> GetEmptySlotNumber()
        {
            try
            {
                return ownerRepository.GetEmptySlot();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
