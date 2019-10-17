using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesparcheBackend.BO;
using DesparcheBackend.BO.BlobStorage;
using DesparcheBackend.BO.Utilidades;
using DesparcheBackend.DA;
using DesparcheBackend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesparcheBackend.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructoraController : ControllerBase
    {
        readonly IUsuarioBO usuarioBO;
        readonly IBlobStorage blobStorage;
        readonly IDeepApi deepApi;
        public ConstructoraController(PruebaAngularContext sc)
        {
            usuarioBO = new UsuarioBO(new UsuarioDA(sc));
            blobStorage = new BlobStorage();
            deepApi = new DeepApi();
        }

        //[Route("crearusuario")]
        //[HttpGet]
        ////[Authorize]
        //public ActionResult obtenerConstructoras()
        //{
        //    try
        //    {
        //        //return Ok(usuarioBO.crearUsuario(/*model*/));
        //    }
        //    catch (Exception)
        //    {
        //        throw new ArgumentException("Se presentó un error en el controlador");
        //    }
        //}
    }
}