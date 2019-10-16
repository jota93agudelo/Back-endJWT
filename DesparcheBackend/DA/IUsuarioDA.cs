using DesparcheBackend.Controllers;
using DesparcheBackend.ModelsTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesparcheBackend.DA
{
    public interface IUsuarioDA
    {
        IList<UsuarioTO> consultaUsuarios();
        string login(LoginModel model);
        bool crearUsuario(LoginModel model);
    }
}
