using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ParkingLotModelLayer;
namespace RepositoryLayer
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly string connectionString;

        public IConfiguration Configuration { get; }

        public OwnerRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetSection("DBConnection").GetSection("ConnectionString").Value;
        }

        public IEnumerable<SlotInformation> GetEmptySlot()
        {
            List<SlotInformation> slotNumberList = new List<SlotInformation>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spGetSlotNumber", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SlotInformation slotInformation = new SlotInformation();
                    slotInformation.Id = Convert.ToInt32(reader["Id"]);
                    slotInformation.SlotNumber = Convert.ToInt32(reader["SlotNumber"]);
                    slotNumberList.Add(slotInformation);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return slotNumberList;
        }
    }
}
