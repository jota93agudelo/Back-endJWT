using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesparcheBackend.BO;
using DesparcheBackend.DA;
using DesparcheBackend.DB;
using DesparcheBackend.Models;
using DesparcheBackend.ModelsTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesparcheBackend.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //private readonly DbContext bDContext;
        readonly IUsuarioBO usuarioBO;
        public ValuesController(PruebaAngularContext sc)
        {
            usuarioBO = new UsuarioBO(new UsuarioDA(sc));
        }
        // GET api/values
        [HttpGet]
        //[Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("prueba")]
        [HttpGet]
        [Authorize]
        public ActionResult Prueba()
        {
            try
            {
                return Ok(usuarioBO.consultaUsuarios());
            }
            catch (Exception)
            {
                throw new ArgumentException("Se presentó un error en el controlador");
            }
        }

        [Route("crearusuario")]
        [HttpGet]
        //[Authorize]
        public ActionResult crearUsuario()
        {
            try
            {
                LoginModel model = new LoginModel();
                return Ok(usuarioBO.crearUsuario(model));
            }
            catch (Exception)
            {
                throw new ArgumentException("Se presentó un error en el controlador");
            }
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                return Ok(usuarioBO.login(model));
            }
            catch (Exception)
            {
                throw new ArgumentException("Se presentó un error en el controlador");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
