
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

    public class ParkingRepository : IParkingRepository
    {
        private readonly string connectionString;

        public IConfiguration Configuration { get; }
        
        public ParkingRepository(IConfiguration configuration)
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
                    parkingLot.ParkingType = Convert.ToInt32(reader["ParkingType"]);
                    parkingLot.DriverType = Convert.ToInt32(reader["DriverType"]);
                    parkingLot.VehicleType = Convert.ToInt32(reader["VehicleType"]);                                        
                    parkingLot.SlotNumber = Convert.ToInt32(reader["SlotNumber"]);                    
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

        public IEnumerable<ParkingLot> searchParkingData(string vehicleNumber)
        {
            List<ParkingLot> lstParkingLot = new List<ParkingLot>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spSearchVehicle", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@VehicleNumber", vehicleNumber);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ParkingLot parkingLot = new ParkingLot();
                    parkingLot.Id = Convert.ToInt32(rdr["Id"]);
                    parkingLot.VehicleNumber = rdr["VehicleNumber"].ToString();
                    parkingLot.ParkingType = Convert.ToInt32(rdr["ParkingType"]);
                    parkingLot.DriverType = Convert.ToInt32(rdr["DriverType"]);
                    parkingLot.VehicleType = Convert.ToInt32(rdr["VehicleType"]);                    
                    parkingLot.SlotNumber = Convert.ToInt32(rdr["SlotNumber"]);
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

        public IEnumerable<ParkingLot> searchParkingDataBySlotNumber(int slotNumber)
        {
            List<ParkingLot> lstParkingLot = new List<ParkingLot>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spSearchVehicleBySlotNumber", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SlotNumber",slotNumber);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ParkingLot parkingLot = new ParkingLot();
                    parkingLot.Id = Convert.ToInt32(rdr["Id"]);
                    parkingLot.VehicleNumber = rdr["VehicleNumber"].ToString();
                    parkingLot.ParkingType = Convert.ToInt32(rdr["ParkingType"]);
                    parkingLot.DriverType = Convert.ToInt32(rdr["DriverType"]);
                    parkingLot.VehicleType = Convert.ToInt32(rdr["VehicleType"]);
                    parkingLot.SlotNumber = Convert.ToInt32(rdr["SlotNumber"]);
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

        public Boolean Unpark(string vehicleNumber)
        {
            List<ParkingLot> lstParkingLot = new List<ParkingLot>();
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spUnpark", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleNumber", vehicleNumber);
                con.Open();
                
                int result=cmd.ExecuteNonQuery();
                if (result > 0)
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
            
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spPark", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleNumber", parkingLot.VehicleNumber);
                cmd.Parameters.AddWithValue("@ParkingType", parkingLot.ParkingType);
                cmd.Parameters.AddWithValue("@VehicleType", parkingLot.VehicleType);
                cmd.Parameters.AddWithValue("@DriverType", parkingLot.DriverType);
                cmd.Parameters.AddWithValue("@SlotNumber", parkingLot.SlotNumber);
                con.Open();
                
                int result=cmd.ExecuteNonQuery();

                if (result > 0)
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
