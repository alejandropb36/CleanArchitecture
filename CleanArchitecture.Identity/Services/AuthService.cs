using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest authRequest)
        {
            var user = await _userManager.FindByEmailAsync(authRequest.Email);

            if (user == null)
            {
                throw new Exception($"El usuario con email {authRequest.Email} no existe");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, authRequest.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"Las credenciales son incorrectas");
            }

            var token = await GenerateToken(user);
            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return authResponse;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest registrationRequest)
        {
            var existingUser = await _userManager.FindByNameAsync(registrationRequest.Username);
            if (existingUser != null)
            {
                throw new Exception($"El usuario {registrationRequest.Username} ya existe");
            }

            var existingEmail = await _userManager.FindByEmailAsync(registrationRequest.Email);
            if (existingEmail != null)
            {
                throw new Exception($"El usuario con email {registrationRequest.Email} ya existe");
            }

            var user = new ApplicationUser
            {
                Email = registrationRequest.Email,
                UserName = registrationRequest.Username,
                Nombre = registrationRequest.Nombre,
                Apellidos = registrationRequest.Apellidos,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registrationRequest.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"{result.Errors}");
            }
            
            await _userManager.AddToRoleAsync(user, "Operator");
            var token = await GenerateToken(user);
            return new RegistrationResponse
            {
                Email = user.Email,
                UserName = user.UserName,
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimsTypes.Uid, user.Id)
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }
    }
}
