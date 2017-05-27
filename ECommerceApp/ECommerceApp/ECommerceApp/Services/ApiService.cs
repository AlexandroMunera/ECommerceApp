using ECommerceApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("Http://zulu-software.com");
                var url = "/ECommerce/api/Products";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(result);

                return products.OrderBy(p => p.Description).ToList();
            
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Customer>> GetCustomers()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("Http://zulu-software.com");
                var url = "/ECommerce/api/Customers";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<Customer>>(result);

                return customers.OrderBy(p => p.CustomerId).ThenBy
                    ( p => p.FirstName).ThenBy(p=>p.LastName).ToList().Take(20).ToList();

            }
            catch
            {
                return null;
            }
        }
    }
}
