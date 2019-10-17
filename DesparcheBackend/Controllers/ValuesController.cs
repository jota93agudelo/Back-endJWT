using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesparcheBackend.BO;
using DesparcheBackend.BO.BlobStorage;
using DesparcheBackend.BO.Utilidades;
using DesparcheBackend.DA;
using DesparcheBackend.DB;
using DesparcheBackend.Models;
using DesparcheBackend.ModelsTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DesparcheBackend.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //private readonly DbContext bDContext;
        readonly IUsuarioBO usuarioBO;
        readonly IBlobStorage blobStorage;
        readonly IDeepApi deepApi;
        public ValuesController(PruebaAngularContext sc )
        {
            usuarioBO = new UsuarioBO(new UsuarioDA(sc));
            blobStorage = new BlobStorage();
            deepApi = new DeepApi();
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
        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                var prueba2 = deepApi.CompararImagen();
                var prueba = blobStorage.SubirArchivoSoporte("", "", "", true);
                return Ok("Dio");
            }
            catch (Exception ex)
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
