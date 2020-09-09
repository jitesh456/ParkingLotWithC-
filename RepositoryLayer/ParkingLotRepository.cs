
namespace RepositoryLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using ParkingLotModelLayer;

    public class ParkingLotRepository : IParkingLotRepository
    {
        private readonly string connectionString;

        public IConfiguration Configuration { get; }
        
        public ParkingLotRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetSection("DBConnection").GetSection("ConnectionString").Value;
        }

        public IEnumerable<ParkingLot> GetAllParkingData()
        {
            List<ParkingLot> lstParkingLot = new List<ParkingLot>();
            SqlConnection con=null;
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spGetAllParking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ParkingLot parkingLot = new ParkingLot();
                    parkingLot.Id = Convert.ToInt32(reader["Id"]);
                    parkingLot.VehicleNumber = reader["VehicleNumber"].ToString();
                    parkingLot.EntryTime = Convert.ToDateTime(reader["EntryTime"]);
                    parkingLot.ParkingType = Convert.ToInt32(reader["ParkingType"]);
                    parkingLot.DriverType = Convert.ToInt32(reader["DriverType"]);
                    parkingLot.VehicleType = Convert.ToInt32(reader["VehicleType"]);
                    parkingLot.Disable = Convert.ToBoolean(reader["Disable"]);
                    parkingLot.ExitTime = Convert.ToDateTime(reader["ExitTime"]);
                    parkingLot.SlotNumber = Convert.ToInt32(reader["SlotNumber"]);
                    parkingLot.ModifiedTime = Convert.ToDateTime(reader["ModifiedTime"]);
                    lstParkingLot.Add(parkingLot);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally {
                con.Close();
            }
                
            return lstParkingLot;
        }

        public IEnumerable<ParkingLot> searchParkingData(string SearchField)
        {
            List<ParkingLot> lstParkingLot = new List<ParkingLot>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spSearchVehicle", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("SearchField", SearchField);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ParkingLot parkingLot = new ParkingLot();
                    parkingLot.Id = Convert.ToInt32(rdr["Id"]);
                    parkingLot.VehicleNumber = rdr["VehicleNumber"].ToString();
                    parkingLot.EntryTime = Convert.ToDateTime(rdr["EntryTime"]);
                    parkingLot.ParkingType = Convert.ToInt32(rdr["ParkingType"]);
                    parkingLot.DriverType = Convert.ToInt32(rdr["DriverType"]);
                    parkingLot.VehicleType = Convert.ToInt32(rdr["VehicleType"]);
                    parkingLot.Disable = Convert.ToBoolean(rdr["Disable"]);
                    parkingLot.ExitTime = Convert.ToDateTime(rdr["ExitTime"]);
                    parkingLot.SlotNumber = Convert.ToInt32(rdr["SlotNumber"]);
                    parkingLot.ModifiedTime = Convert.ToDateTime(rdr["ModifiedTime"]);
                    lstParkingLot.Add(parkingLot);
                }
                con.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lstParkingLot;
        }

        public Boolean Unpark(string vehicleNumber,DateTime modifiedTime )
        {
            List<ParkingLot> lstParkingLot = new List<ParkingLot>();
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spUnPark", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleNumber", vehicleNumber);
                cmd.Parameters.AddWithValue("@ModiFiedTime", modifiedTime);
                con.Open();
                int result=cmd.ExecuteNonQuery();
                if (result > 1)
                {
                    return true;
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
            return false;
        }


        public Boolean Park(ParkingLot parkingLot)
        {
            List<ParkingLot> lstParkingLot = new List<ParkingLot>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spUnPark", con);
                cmd.Parameters.AddWithValue("@VehicleNumber", parkingLot.VehicleNumber);
                cmd.Parameters.AddWithValue("@EntryTime", parkingLot.EntryTime);
                cmd.Parameters.AddWithValue("@ParkingType", parkingLot.ParkingType);
                cmd.Parameters.AddWithValue("@VehicleType", parkingLot.VehicleType);
                cmd.Parameters.AddWithValue("@DriverType", parkingLot.DriverType);
                cmd.Parameters.AddWithValue("@Disable", parkingLot.Disable);
                cmd.Parameters.AddWithValue("@ExitTime", parkingLot.ExitTime);
                cmd.Parameters.AddWithValue("@SlotNumber", parkingLot.SlotNumber);
                cmd.Parameters.AddWithValue("@ModifiedTime", parkingLot.ModifiedTime);
                con.Open();
                
                int result=cmd.ExecuteNonQuery();
                if (result > 1)
                {
                    return true;
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
            return false;
        }
    }
}
