using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APICube.Models.EntityFramework;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using S401A2.Model;
using S401A2.Model.EntityFramework;

namespace S401A2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly CubeDBContext _context;

        public LoginController(IConfiguration config, CubeDBContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var existingClient = _context.Clients.SingleOrDefault(c => c.Email == request.Email);
            if (existingClient != null)
            {
                return BadRequest("Cet email est déjà utilisé.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newClient = new Client
            {
                CiviliteId = request.CiviliteId,
                Nom = request.Nom,
                Prenom = request.Prenom,
                Email = request.Email,
                Mdp = hashedPassword,
                DateNaissance = request.DateNaissance,
                Role = 2
            };

            _context.Clients.Add(newClient);
            _context.SaveChanges();

            return Ok(new { message = "Client créé avec succès !" });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var client = _context.Clients.SingleOrDefault(c => c.Email == loginRequest.Email);

            if (client == null)
            {
                return Unauthorized("Identifiants incorrects");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, client.Mdp);

            if (!isPasswordValid)
            {
                return Unauthorized("Identifiants incorrects");
            }

            string stringRole = (client.Role == 1) ? Policies.Admin : Policies.User;

            var tokenString = GenerateJwtToken(client, stringRole);
            return Ok(new { token = tokenString });
        }

        private string GenerateJwtToken(Client client, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string fullName = $"{client.Prenom} {client.Nom}";

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, client.Email),
            new Claim("fullName", fullName),
            new Claim(ClaimTypes.Role, role),
            new Claim("IdClient", client.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public int CiviliteId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Le mot de passe en clair tapé par l'utilisateur
        public DateTime? DateNaissance { get; set; }
    }
}
