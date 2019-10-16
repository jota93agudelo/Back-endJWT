using DesparcheBackend.Controllers;
using DesparcheBackend.Models;
using DesparcheBackend.ModelsTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesparcheBackend.DA
{
    public class UsuarioDA:IUsuarioDA
    {
        private readonly PruebaAngularContext BDContext;
        public UsuarioDA(PruebaAngularContext BDContext)
        {
            this.BDContext = BDContext;
        }

        public IList<UsuarioTO> consultaUsuarios()
        {
            try
            {
                IList<Usuario> listaUsuarios = BDContext.Usuario.ToList();
                IList<UsuarioTO> listaUsuariosTO = new List<UsuarioTO>();
                foreach (Usuario usuario in listaUsuarios)
                {
                    UsuarioTO usuarioTO = new UsuarioTO
                    {
                        Apellidos = usuario.Apellidos,
                        Nombre = usuario.Nombre,
                        Correo = usuario.Correo
                    };
                    listaUsuariosTO.Add(usuarioTO);
                }
                return listaUsuariosTO;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Se presentó un error al momento de consultar los usuarios");
            }
        }

        public string login(LoginModel model)
        {
            try
            {
                //var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
                return "";
            }
            catch (Exception ex )
            {
                throw new ArgumentException ("Se presentó un error al momento de realizar el login");
            }
        }

        public bool crearUsuario(LoginModel model)
        {
            try
            {
                
                var manager = new UserStore<Usuario>(BDContext);
                Usuario usuario = new Usuario
                {
                    Email = "juan.jose.agudelo@accenture.com",
                    Apellidos = "Agudelo",
                    Contrasena = "1234567890",
                    Correo = "juan.jose.agudelo@accenture.com",
                    Nombre = "Juan Jose",
                    UserName = "juan.jose.agudelo"
                };
                IdentityResult result = manager.CreateAsync(usuario).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    throw new ArgumentException("Mensaje El usuario no se registró correctamente o ya existe en la base de datos");
                }
                manager.SetPasswordHashAsync(usuario, "123456789");
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Se presentó un error al momento de realizar el login");
            }
        }
    }
}
