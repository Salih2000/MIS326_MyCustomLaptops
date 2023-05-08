using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS326_MyCustomLaptops.Dtos;
using System.Data.SqlClient;

namespace MIS326_MyCustomLaptops.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    string constr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MIS326_MyCustomLaptopsdb;integrated security=true";
    [ HttpPost]
    public IActionResult Register(UserRegisterDto userRegisterDto)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "SELECT * FROM Customers";
            List<UserRegisterDto> fruits = new List<UserRegisterDto>();
            using (SqlCommand cmd = new SqlCommand(query))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Customers (Name, Email, Phone, Password) VALUES (@Value1, @Value2, @Value3, @Value4)", con))
                {
                    // Set the parameter values
                    command.Parameters.AddWithValue("@Value1", userRegisterDto.Name);
                    command.Parameters.AddWithValue("@Value2", userRegisterDto.Email);
                    command.Parameters.AddWithValue("@Value3", userRegisterDto.PhoneNumber);
                    command.Parameters.AddWithValue("@Value4", userRegisterDto.Password);

                    // Execute the SQL statement
                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine("Rows affected: " + rowsAffected);
                }
                con.Close();

            }
            return Ok(fruits);
        }
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "SELECT * FROM Customers";
            List<UserRegisterDto> fruits = new List<UserRegisterDto>();
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        fruits.Add(new UserRegisterDto
                        {
                            Name = sdr["Name"].ToString(),
                            Email = sdr["Email"].ToString(),
                            PhoneNumber = sdr["Phone"].ToString(),

                        });
                    }
                }
                con.Close();

            }
            return Ok(fruits);
        }
           
    }
}
