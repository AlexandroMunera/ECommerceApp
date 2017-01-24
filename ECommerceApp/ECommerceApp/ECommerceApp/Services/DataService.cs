using ECommerceApp.Data;
using ECommerceApp.Models;
using System;

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

    }
}
