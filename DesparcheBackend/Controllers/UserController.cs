using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DesparcheBackend.Models;
using DesparcheBackend.ModelsTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DesparcheBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private readonly ApplicationsSettings _appSettings;
        public UserController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IOptions<ApplicationsSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }


        [HttpPost]
        [Route("registrar")]
        public async Task<Object> Registrar(UsuarioTO usuarioTO)
        {
            try
            {
                usuarioTO.Rol = "Consumidor";
                Usuario usuario = new Usuario
                {
                    Apellidos = usuarioTO.Apellidos,
                    Contrasena = usuarioTO.Contrasena,
                    Correo = usuarioTO.Correo,
                    Nombre = usuarioTO.Nombre,
                    UserName = usuarioTO.Correo.Split('@')[0]
                };
                var result = await _userManager.CreateAsync(usuario, usuarioTO.Contrasena);
                await _userManager.AddToRoleAsync(usuario, usuarioTO.Rol);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult>Login(LoginModel loginModel)
        {
            try
            {
                var usuario = await _userManager.FindByNameAsync(loginModel.Correo.Split('@')[0]);
                if (usuario != null && await _userManager.CheckPasswordAsync(usuario, loginModel.Contrasena))
                {
                    // Obtener rol del usuario

                    var role = await _userManager.GetRolesAsync(usuario);
                    IdentityOptions _options = new IdentityOptions();

                    var tokenDescripcion = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserId", usuario.Id.ToString()),
                            new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescripcion);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }
                else
                {
                    return BadRequest(new { message= "Usuario o contraseña incorrecto" });
                }

            }
            catch (Exception ex )
            {

                throw;
            }
        }


    }
}