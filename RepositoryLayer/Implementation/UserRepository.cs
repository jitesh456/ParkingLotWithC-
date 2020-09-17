using Microsoft.Extensions.Configuration;
using ParkingLotModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Implementation
{
    public class UserRepository:IUserRepository
    {
        private readonly string connectionString;

        public IConfiguration Configuration { get; }

        public UserRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetSection("DBConnection").GetSection("ConnectionString").Value;
        }

        public Boolean AddUser(UserDetails userDetails)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Role", userDetails.Role);
                cmd.Parameters.AddWithValue("@Email", userDetails.Email);
                cmd.Parameters.AddWithValue("@Password", userDetails.Password);
                con.Open();
           
                int result = cmd.ExecuteNonQuery();

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

        public string Login(User userInfo)
        {
            string RoleName = "";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("spLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", userInfo.Email);
                cmd.Parameters.AddWithValue("@Password", userInfo.Password);
                cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 20);
                cmd.Parameters["@Role"].Direction = ParameterDirection.Output;                
                con.Open();

                int result = cmd.ExecuteNonQuery();

                if (result < 0)
                {
                    RoleName = Convert.ToString(cmd.Parameters["@Role"].Value).Trim();
                    return RoleName;
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
            return RoleName;
        }
    }
}
