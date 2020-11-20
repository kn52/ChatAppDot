using ChatApplicationRepository.Infc;
using ChatApplicationModel.Dto;
using ChatApplicationModel.Model;
using ChatApplicationModel.Util;
using ChatApplicationModel.Util.Infc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ChatApplicationRepository.Impl
{
    public class LoginRepository : ILoginRepository
    {
        public LoginRepository(IConfiguration configuration, IMessagingService messagingService)
        {
            this.MessagingService = messagingService;
            this.Configuration = configuration;
            DBString = this.Configuration["ConnectionString:DBConnection"];
        }
        public IConfiguration Configuration { get; set; }
        public IMessagingService MessagingService { get; set; }

        private readonly string DBString = null;

        public string Registration(User user)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                var keyNew = SaltGenerator.GeneratePassword(10);
                var newpassword = SaltGenerator.Base64Encode(
                SaltGenerator.EncodePassword(user.password, keyNew));

                using (SqlCommand cmd = new SqlCommand("spRegistration", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@FirstName", user.firstName);
                    cmd.Parameters.AddWithValue("@LastName", user.lastName);
                    cmd.Parameters.AddWithValue("@Email", user.email);
                    cmd.Parameters.AddWithValue("@Password", newpassword);
                    cmd.Parameters.AddWithValue("@PhoneNumber", user.phoneNumber);
                    cmd.Parameters.AddWithValue("@KeyNew", keyNew);
                    
                    try
                    {
                        conn.Open();
                        int id = cmd.ExecuteNonQuery();
                        if (id > 0)
                        {
                            MessagingService.Send("Registration", "Registered Successfully With Email Id " + user.email,
                                "ashish52922@gmail.com");
                            return "Registered Successfully";
                        }
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return "";
        }
        public User Login(LoginDto loginDto)
        {
            using (SqlConnection conn = new SqlConnection(this.DBString))
            {
                using (SqlCommand cmd = new SqlCommand("spLogin", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@Email", loginDto.email);
                    
                    try
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            User user = new User();
                            while (rdr.Read())
                            {
                                //var epass = SaltGenerator.EncodePassword(loginDto.password, rdr["KeyNew"].ToString());
                                //var dpass = SaltGenerator.Base64Decode(rdr["Password"].ToString());
                                //if (epass.Equals(dpass))
                                //{
                                    user.id = Convert.ToInt32(rdr["id"]);
                                    user.firstName = rdr["FirstName"].ToString();
                                    user.lastName = rdr["LastName"].ToString();
                                    user.email = rdr["Email"].ToString();
                                    user.password = rdr["Password"].ToString();
                                    user.phoneNumber = rdr["PhoneNumber"].ToString();
                                //}
                                return user;
                            }
                            return null;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }
        public string GenerateJSONWebToken(int id)
        {
            return TokenGenerator.GenerateJSONWebToken(id,Configuration);
        }
    }
}
