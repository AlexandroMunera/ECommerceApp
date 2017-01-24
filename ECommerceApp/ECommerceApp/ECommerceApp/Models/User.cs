using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ECommerceApp.Models
{
    public class User
    {
        [PrimaryKey]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Photo { get; set; }
        
        public string Phone { get; set; }

        public string Address { get; set; }
        
        public int CityId { get; set; }

        public int CompanyId { get; set; }

        [ManyToOne]
        public Company Company { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsUser { get; set; }

        public bool IsCurstomer { get; set; }

        public bool IsSupplier { get; set; }

        public bool IsRemembered { get; set; }

        public string Password { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        //public string PhotoFullPath { get { return "http://zulu-software.com/ECommerce/Content/Users/7.jpg"; } }

        //public string PhotoFullPath { get { return "https://pbs.twimg.com/profile_images/2283801410/qpylci2q7xhqaaes8g1t_400x400.jpeg"; } }

        public string PhotoFullPath { get { return "https://avatars0.githubusercontent.com/u/6957621?v=3&s=400.png "; } }

           
        
        //public string PhotoFullPath { get { return string.Format("Aquí va la url donde esta la foto", Photo.Substring(1)); } }

        public override int GetHashCode()
        {
            return UserId;
        }
    }
}
