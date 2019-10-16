using DesparcheBackend.Controllers;
using DesparcheBackend.DA;
using DesparcheBackend.ModelsTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesparcheBackend.BO
{
    public class UsuarioBO:IUsuarioBO
    {
        private readonly IUsuarioDA usuarioDA;

        public UsuarioBO(IUsuarioDA usuarioDA)
        {
            this.usuarioDA = usuarioDA;
        }

        public IList<UsuarioTO> consultaUsuarios()
        {
            return usuarioDA.consultaUsuarios();
        }

        public string login(LoginModel model)
        {
            return usuarioDA.login(model);
        }

        public bool crearUsuario(LoginModel model)
        {
            return usuarioDA.crearUsuario(model);
        }

    }
}
