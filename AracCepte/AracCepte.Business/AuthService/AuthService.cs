using AracCepte.Entity.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace AracCepte.Business.AuthService
{
    public class AuthService
    {
        private readonly List<User> _users = new()
        {
            new User
            {
                Id = 1,
                Name = "Ömer",
                Surname = "Demirci",
                Email = "omer.demirci@gmail.com",
                Password = "123456",
                PhoneNumber = 1234567890,
                UserType = "VehicleOwner",
                ImageURL1 = null
            },

            new User
            {
                Id=2,
                Name = "Zehra",
                Surname = "Kozan",
                Email = "zehra.kozan@gmail.com",
                Password = "123456",
                PhoneNumber = 987654321,
                UserType = "Renter",
                ImageURL1 = null

            },

            new User
            {
                Id= 3,
                Name = "Elif Buket",
                Surname = "Duman",
                Email= "e.buket.duman@gmail.com",
                Password = "123456",
                PhoneNumber = 2143657898,
                UserType = "Admin",
                ImageURL1 = null


            }
        };
        private readonly string _key = "Od17@zK11_eBDd#Ez10&InSZ!67-5851";

        public string Authenticate(string email, string password)
        {
            var user = _users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
                    new Claim(ClaimTypes.Role, user.UserType ??  "User"),
                    new Claim("Email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
