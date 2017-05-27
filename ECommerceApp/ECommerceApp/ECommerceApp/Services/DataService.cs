using ECommerceApp.Data;
using ECommerceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceApp.Services
{
    public class DataService
    {
        public User GetUser()
        {
            using (var da = new DataAccess())
            {
                return da.First<User>(true);
            }
        }

        public Response InsertUser(User user)
        {
            try
            {
                using(var da = new DataAccess())
                {
                    var oldUser = da.First<User>(false);

                    if (oldUser != null)
                    {
                        da.Delete(oldUser);
                    }

                    da.Insert(user);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario insertado Ok",
                    Result = user
                };
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

        public Response UpdateUser(User user)
        {
            try
            {
                using (var da = new DataAccess())
                {                   
                    da.Update(user);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario actualizado Ok",
                    Result = user
                };
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

        public void SaveProducts(List<Product> products)
        {
            using(var da = new DataAccess())
            {
                var oldProducts = da.GetList<Product>(false);

                foreach (var p in oldProducts)
                {
                    da.Delete(p);
                }

                foreach (var product in products)
                {
                    da.Insert(product);
                }
            }
        }

        public List<Product> GetProducts(string filter)
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Product>(true).OrderBy(p => p.Description).Where(p => p.Description.ToUpper().Contains(filter.ToUpper())).ToList();
            }
        }

        public List<Product> GetProducts()
        {
            using(var da = new DataAccess())
            {
                return da.GetList<Product>(true).OrderBy(p => p.Description).ToList();
            }
        }

        public Response Login(string email, string password)
        {
            try
            {
                using(var da = new DataAccess())
                {
                    var user = da.First<User>(true);
                    if (user == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No hay conexión y no hay un usuario previamente registrado."
                        };
                    }

                    if (user.UserName.ToUpper() == email.ToUpper() && user.Password == password)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                            Message = "ok",
                            Result = user

                        };
                    }

                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o contraseña incorrecto"
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
               

        public void SaveCustomers(List<Customer> customers)
        {
            using (var da = new DataAccess())
            {
                var oldCustomers = da.GetList<Customer>(false);

                foreach (var customer in oldCustomers)
                {
                    da.Delete(customer);
                }

                foreach (var customer in customers)
                {
                    da.Insert(customer);
                }
            }
        }

        public  List<Customer> GetCustomers()
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Customer>(true).OrderBy(p => p.FirstName).ThenBy(p => p.LastName).ToList();
            }
        }

        public List<Customer> GetCustomers(string filter)
        {
            using (var da = new DataAccess())
            {
                return da.GetList<Customer>(true).OrderBy(p => p.FirstName).ThenBy(p=>p.LastName)
                    .Where(p => p.FirstName.ToUpper().Contains(filter.ToUpper()) ||  p.LastName.ToUpper().Contains(filter.ToUpper())).ToList();
            }
        }
    }
}
