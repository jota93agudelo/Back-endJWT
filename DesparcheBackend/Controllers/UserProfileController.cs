using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesparcheBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DesparcheBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<Usuario> _userManager;

        public UserProfileController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<Object> ObtenerPerfilUsuario()
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                var user = await _userManager.FindByIdAsync(userId);

                return new
                {
                    user.Correo,
                    user.Nombre,
                    user.UserName
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Authorize(Roles ="Administrador")]
        [Route("administrador")]
        public string Administrador()
        {
            return "Servicio para administrador";
        }

        [HttpGet]
        [Authorize(Roles = "Consumidor,Perito")]
        [Route("consumidor")]
        public string Consumidor()
        {
            return "Servicio para consumidor";
        }

        [HttpGet]
        [Authorize(Roles = "Consumidor,Administrador")]
        [Route("ambos")]
        public string Ambos()
        {
            return "Servicio para varios roles";
        }
    }
}