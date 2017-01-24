using ECommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Services
{
    public class ApiService
    {
        public async Task<Response> Login(string email, string password)
        {
            try
            {
                //Aquí tengo que usar el Azure, pendiente que me den la cuenta
                if (email.Equals("alexandromunera@gmail.com") && password.Equals("123"))
                {

                    var user = new User
                    {
                        FirstName = "Alexandro",
                        LastName = "Múnera",
                        Email = "alexandromunera@gmail.com"
                    };

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Login ok.",
                        Result = user
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario y contraseña no validos."
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message                
                };
            }
        }
    }
}
