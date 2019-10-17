using DeepAI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DesparcheBackend.BO.Utilidades
{
    public class DeepApi:IDeepApi
    {

        public string CompararImagen()
        {
            DeepAI_API api = new DeepAI_API(apiKey: "quickstart-QUdJIGlzIGNvbWluZy4uLi4K");

            StandardApiResponse resp = api.callStandardApi("image-similarity", new
            {
                image1 = File.OpenRead(@"C:\Users\juan.jose.agudelo\source\repos\DesparcheBackend\DesparcheBackend\temp\imagen1.jpeg"),
                image2 = File.OpenRead(@"C:\Users\juan.jose.agudelo\source\repos\DesparcheBackend\DesparcheBackend\temp\imagen2.jpeg"),
            });
            string diferencia = api.objectAsJsonString(resp.output);
            return diferencia;
        }
    }
}
